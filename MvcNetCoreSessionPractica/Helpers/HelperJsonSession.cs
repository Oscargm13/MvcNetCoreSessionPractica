using Newtonsoft.Json;

namespace MvcNetCoreSessionPractica.Helpers
{
    public class HelperJsonSession
    {
        //VAMOS A UTILIZAR EL METODO GetString() COMO HERRAMIENTA
        //ALMACENAMOS OBJETOS CON Serialize DE JSON
        public static string SerializeObject<T>(T data)
        {
            //CONVERTIMOS EL OBJETO A STRING
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        //recibiremos un string y lo sonvertimos a cualquier objeto T
        public static T DeserializeObject<T>(string data)
        {
            //DESERALIZAMOS EL STRING A T
            T objeto = JsonConvert.DeserializeObject<T>(data);
            return objeto;
        }
    }
}
