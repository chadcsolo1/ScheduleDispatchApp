using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;

namespace Jobs.Infrastructure.Services.DataShaping
{
    public sealed class DataShapingService
    {
        // Cache of PropertyInfo arrays keyed by type, so reflection only needs to run once per (Type, requested-fields)
        // combination during the lifetime of the application. Reflection calls like GetProperties() are relatively
        // expensive, so caching avoids repeating that cost on every request.
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> PropertyCache = new();

        public List<ExpandoObject> ShapeData<T>(IEnumerable<T> entities, string fields)
        {
                 // Parse the comma-separated "fields" string (e.g. "Id,Name,City") into a normalized set of field names.
            // - Split on commas and remove empty entries (handles trailing/extra commas).
            // - Trim whitespace around each field name.
            // - Store in a HashSet with case-insensitive comparison so lookups are fast (O(1)) and case doesn't matter.
            
            var fieldSet = fields
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(f => f.Trim())
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            // Get all of the properties for the type T (Job, Organization, User, etc.)
            // We are going to only keep properties listed in fields parameter.
            // Cache the properties because using reflection is expensive:
            // - GetOrAdd checks the cache first for typeof(T); if not present, it runs the factory delegate.
            // - The factory reflects over T's public instance properties and filters down to only those
            //   whose names appear in the requested fieldSet.
            // NOTE: because the cache key is only the Type (not the fields string), if this method is called
            // again for the same T but with a *different* fields value, the cached (first) property set will
            // be reused instead of being recalculated for the new fields.
            PropertyInfo[] properties = PropertyCache.GetOrAdd(
                typeof(T),
                t => t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => fieldSet.Contains(p.Name))
                    .ToArray());

            // Prepare the list that will hold one shaped (dynamic) object per input entity.
            List<ExpandoObject> shapedData = [];

            // Iterate over every entity passed in.
            foreach (var entity in entities)
            {
                // Create a new dynamic object (backed by ExpandoObject) to hold only the requested properties
                // for this entity. Using IDictionary<string, object?> lets us add properties by name at runtime.
                IDictionary<string, object?> shapedObject = new ExpandoObject();

                // For each filtered property, read its value off the current entity via reflection
                // and copy it into the dynamic object under the same property name.
                foreach (var prop in properties)
                {
                    shapedObject[prop.Name] = prop.GetValue(entity);
                }

                // Cast the dictionary back to ExpandoObject and add it to the result list.
                shapedData.Add((ExpandoObject) shapedObject);
            }

            // Return the list of shaped (partial) objects, one per input entity, each containing only
            // the fields the caller requested.
            return shapedData;
        }
    }
}
