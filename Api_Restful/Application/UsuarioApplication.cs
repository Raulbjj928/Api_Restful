using Api_Restful.Context;
using Api_Restful.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Api_Restful.Application
{
    public class UsuarioApplication
    {
        private ApiContext _context;

        public UsuarioApplication(ApiContext context)
        {
            _context = context;
        }

        public string InsertUser(Usuarios usuario)
        {
            try
            {
                if (usuario != null)
                {
                    var usuarioExiste = GetUserByEmail(usuario.Email);

                    if (usuarioExiste == null)
                    {
                        _context.Add(usuario);
                        _context.SaveChanges();

                        return "Usuário cadastrado com sucesso!";
                    }
                    else
                    {
                        return "Email já cadastrado na base de dados.";
                    }
                }
                else
                {
                    return "Usuário inválido!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string UpdateUser(Usuarios usuario)
        {
            try
            {
                if (usuario != null)
                {
                    _context.Update(usuario);
                    _context.SaveChanges();

                    return "Usuário alterado com sucesso!";
                }
                else
                {
                    return "Usuário inválido!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public Usuarios GetUserByEmail(string email)
        {
            Usuarios primeiroUsuario = new Usuarios();

            try
            {
                if (email == string.Empty)
                {
                    return null;
                }

                var cliente = _context.Usuarios.Where(x => x.Email == email).ToList();
                primeiroUsuario = cliente.FirstOrDefault();

                if (primeiroUsuario != null)
                {
                    return primeiroUsuario;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Usuarios> GetAllUsers()
        {
            List<Usuarios> listaDeUsuarios = new List<Usuarios>();
            try
            {

                listaDeUsuarios = _context.Usuarios.Select(x => x).ToList();

                if (listaDeUsuarios != null)
                {
                    return listaDeUsuarios;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string DeleteUserByEmail(string email)
        {
            try
            {
                if (email == string.Empty)
                {
                    return "Email inválido! Por favor tente novamente.";
                }
                else
                {
                    var usuario = GetUserByEmail(email);

                    if (usuario != null)
                    {
                        _context.Usuarios.Remove(usuario);
                        _context.SaveChanges();

                        return "Usuário " + usuario.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Usuário não cadastrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }
    }
}
