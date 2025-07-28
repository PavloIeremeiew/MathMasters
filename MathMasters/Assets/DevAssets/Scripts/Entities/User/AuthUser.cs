using System.Linq;

namespace MathMasters
{
    public class AuthUser
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name  => Email.Split("@").First();
    }
}
