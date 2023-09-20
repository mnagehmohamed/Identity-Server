using System.Security.Claims;
using System.Text.Json;

namespace EmployeeLogix.Client.Services
{
    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsfromjwt(string jwt) 
        {
        var claims=new List<Claim>();
        var payload = jwt.Split('.')[1];
            var jsonBytes=barseBase64WithoutPadding(payload);
            var keyvaluePair = JsonSerializer.Deserialize<Dictionary<string,object>>(jsonBytes);
            claims.AddRange(keyvaluePair.Select(kvp=>new Claim(kvp.Key,kvp.Value.ToString())));
            return claims;

        }
        private static byte[] barseBase64WithoutPadding(string base64)
        {
            switch (base64.Length%4)
            {
                case 2: base64 += "==";break;
                case 3: base64 += "=";break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
