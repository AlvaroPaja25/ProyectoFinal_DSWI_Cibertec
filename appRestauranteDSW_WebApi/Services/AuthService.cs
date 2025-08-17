using Microsoft.EntityFrameworkCore;
using appRestauranteDSW_WebApi.Data.Entities;
using appRestauranteDSW_WebApi.DTOs;

namespace appRestauranteDSW_WebApi.Services
{
    public class AuthService
    {
        private readonly RestauranteContext _ctx;
        private readonly TokenService _token;

        public AuthService(RestauranteContext ctx, TokenService token)
        {
            _ctx = ctx;
            _token = token;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            // 1) Busca usuario por correo
            var user = await _ctx.usuario
                .FirstOrDefaultAsync(u => u.correo == request.Correo);

            if (user == null) return null;

            // 2) Verifica contraseña
            // Si guardas en plano (inicialmente): 
            if (user.contrasena != request.Contrasena) return null;

            // Si usas hash:
            // if (!BCrypt.Net.BCrypt.Verify(request.Contrasena, user.Contrasena)) return null;

            // 3) Descubre rol desde Empleado->Cargo (si existe)
            var empleado = await _ctx.empleado
                .Include(e => e.cargo)
                .FirstOrDefaultAsync(e => e.usuario_id == user.id);

            var rol = empleado?.cargo?.nombre ?? "Empleado";

            // 4) Genera token
            var (token, exp) = _token.Generate(user.id.ToString(), user.correo!, rol);

            return new LoginResponse
            {
                Token = token,
                Usuario = user.correo!,
                Rol = rol,
                Expira = exp
            };
        }
    }
}
