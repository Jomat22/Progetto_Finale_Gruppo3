using System.Reflection;

namespace store.api.src.Helper
{
    public static class DataHelper
    {
        public static void InputDataFormatUppercase<T> (T data) where T : class
        {
            try 
            { 
                if (data is null) { return; }

                Type type = data.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();

                foreach (var property in propertyInfos)
                {
                    if (property.CanWrite)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            // Non effettuo cast tradizionale perché scatterebbe un'eccezione qualore non fosse del tipo castato.
                            // "as" invece ritornerà un valore nullo qualorà il property sia di un tipo differente.
                            var value = property.GetValue(data) as string;
                            if (!string.IsNullOrEmpty(value))
                            {
                                property.SetValue(data, value.ToUpper(), null);
                            }
                        }
                        else if (property.PropertyType == typeof(char))
                        {
                            var value = property.GetValue(data);
                            // I "char" non possono essere nulli (perché primitive). Il controllo non è necessario per il settaggio del valore (lo faccio per quietare il compilatore).
                            if (value != null) 
                            {
                                var charValue = (char)value;
                                property.SetValue(data, char.ToUpper(charValue), null);
                            }
                        }
                    }
                }
            } catch(Exception) { throw; }
        }

        public static void InputDataAddTimestamp<T>(T data) where T : class
        {
            try 
            { 
                if (data is null) { return; }

                Type type = data.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();

                foreach (var property in propertyInfos)
                {
                    if (property.Name == "TimestampCreazione" || property.Name == "TimestampModifica")
                    {
                        if (property.PropertyType == typeof(DateTime) && property.CanWrite)
                        {
                            property.SetValue(data, DateTime.UtcNow, null);
                        }
                    }
                }
            } catch(Exception) { throw; }
        }

        public static void InputDataUpdateTimestamp<T>(T data) where T : class
        {
            try 
            { 
                if (data is null) { return; }

                Type type = data.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();

                foreach (var property in propertyInfos)
                {
                    if (property.Name == "TimestampModifica")
                    {
                        if (property.PropertyType == typeof(DateTime) && property.CanWrite)
                        {
                            property.SetValue(data, DateTime.UtcNow, null);
                        }
                    }
                }
            } catch(Exception) { throw; }
        }
    }
}
