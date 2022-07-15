
using AutoMapper;
using swagger_jwt.DTO;
using swagger_jwt.Models;

namespace swagger_jwt.Data
{
    public class AutoMaperProfiles: Profile
    {
        public AutoMaperProfiles()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Usuario, LoginDTO>().ReverseMap();
            CreateMap<Usuario, CreateUsuario>().ReverseMap();
            CreateMap<Categoria,CategoriaDTO>().ReverseMap();
            CreateMap<Producto, ProductoDTO>().ReverseMap();

            CreateMap<Bodega, BodegaDTO>().ReverseMap();

        }
    }
}
