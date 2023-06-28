using System.Data;
using System.Reflection;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // Table Tool
    public static class DataTableHelper
    {
        public static DataTable ListToDataTable<T>(List<T> entities)
        {
            //Check that the entity collection cannot be empty
            if (entities == null || entities.Count < 1)
            {
                throw new Exception("The collection to be converted is empty");
            }
            //Remove all Properties of the first entity
            Type entityType = entities[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //Generate the structure of the DataTable
            //In the production code, the generated DataTable structure should be cached, which is omitted here
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                dt.Columns.Add(entityProperties[i].Name);
            }
            //Add all entities to the DataTable
            foreach (object entity in entities)
            {
                //Check that all entities are of the same type
                if (entity.GetType() != entityType)
                {
                    throw new Exception("The type of the collection element to be converted is inconsistent");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
    }
}
