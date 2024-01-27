using System.Security.Claims;
using System.Security.Principal;

namespace API.Utilities
{
    public static class IdentityExtensions
    {
        public static int GetCodUser(this IIdentity identity)
        {

            var mockClaims = new List<Claim>
             {
                new Claim("CodUser", 34.ToString(), ClaimValueTypes.Integer32)
             };

            var claimsIdentity = new ClaimsIdentity(mockClaims);

            var castedIdentity = (ClaimsIdentity)identity;

            castedIdentity.AddClaims(claimsIdentity.Claims);


            var clain = castedIdentity.Claims
                .Where(x => x.Type.Equals("CodUser"))
                .Select(x => x.Value)
                .FirstOrDefault();

            return int.Parse(clain);
        }
    }
}
