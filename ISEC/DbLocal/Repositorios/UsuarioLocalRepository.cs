using System;
using System.Collections.Generic;
using System.Data.SQLite;
using ISEC.DbLocal.Interfaces;
using DataAccess.Local;
using ISEC.Observer;
using DataAccess;
namespace ISEC.DbLocal.Repositorios
{
    public class UsuarioLocalRepository : IUsuarioLocalRepository
    {
        string cadena = ConnectionLocal.ConecctionString;
        PuestoLocalRepository puestoLocalRepository = new PuestoLocalRepository();
        List<IObserverUsuarioLocal> _Observers;
        public TypeProcess TypeProcess { get; set; }
        public bool IsUpdate { get; set; }
        public UsuarioLocalRepository()
        {
            _Observers = new List<IObserverUsuarioLocal>();
        }


        public List<UsuarioLocal> ConvertServerUsersToLocal(List<Usuario> serverUsers)
        {
            List<UsuarioLocal> localUsers = new List<UsuarioLocal>();
            if (serverUsers.Count > 0)
            {
                foreach (var us in serverUsers)
                {
                    localUsers.Add(new UsuarioLocal()
                    {
                        Id = us.Id,
                        Nombre = us.Nombre,
                        Username = us.Username,
                        Password = us.Password,
                        Sync = us.Sync,
                        Activo = us.Activo,
                        Puesto = us.Puesto,
                        puestoid = us.puestoid,
                        Modulos = null
                    });
                }
            }
            return localUsers;
        }
        public UsuarioLocal ConvertUserServerToLocal(Usuario usuarioServer)
        {
            UsuarioLocal usuario = new UsuarioLocal()
            {
                Nombre = usuarioServer.Nombre,
                Password = usuarioServer.Password,
                puestoid = usuarioServer.puestoid,
                Username = usuarioServer.Username,
                Sync = usuarioServer.Sync,
                Activo = usuarioServer.Activo
            };
            return usuario;
        }
        public bool Add(UsuarioLocal usuarioLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(cadena))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand($"insert into usuarios(nombre,username,password,puestoid,sync) values(@nombre,@username,@pass,@puestoid,@sync)", conn))

                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@nombre", usuarioLocal.Nombre);
                    cmd.Parameters.AddWithValue("@username", usuarioLocal.Username);
                    cmd.Parameters.AddWithValue("@pass", usuarioLocal.Password);
                    cmd.Parameters.AddWithValue("@puestoid", usuarioLocal.puestoid);
                    cmd.Parameters.AddWithValue("@sync", 0);
                    //cmd.Parameters.AddWithValue("@rolid", usuarioLocal.rolid);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }
            }
            return inserted;
        }

        public bool Desactivate(int id)
        {
            throw new NotImplementedException();
        }

        public List<UsuarioLocal> GetAll()
        {
            List<UsuarioLocal> users = new List<UsuarioLocal>();
            using (var conn = new SQLiteConnection(cadena))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand($"select * from usuarios", conn))

                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        UsuarioLocal local = new UsuarioLocal();
                        local.Id = int.Parse(rd["id"].ToString());
                        local.Nombre = rd["nombre"] as string;
                        local.Username = rd["username"] as string;
                        local.Password = rd["password"] as string;
                        local.puestoid = !string.IsNullOrEmpty(rd["puestoid"].ToString()) ? int.Parse(rd["puestoid"].ToString()) : 0;
                        local.Puesto =local.puestoid >0 ? puestoLocalRepository.Get(local.puestoid).Descripcion : "";
                        local.Sync = int.Parse(rd["sync"].ToString());
                        local.Activo = int.Parse(rd["activo"].ToString());
                        //local.rolid = !string.IsNullOrEmpty(rd["rolid"].ToString()) ? int.Parse(rd["rolid"].ToString()) :0;
                        users.Add(local);

                    }
                }
                return users;
            }
        }

        public bool Update(UsuarioLocal usuarioLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(cadena))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand($"update usuarios set nombre=@nombre,username=@username,password=@pass,puestoid=@puestoid WHERE id=@id", conn))

                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", usuarioLocal.Id);
                    cmd.Parameters.AddWithValue("@nombre", usuarioLocal.Nombre);
                    cmd.Parameters.AddWithValue("@username", usuarioLocal.Username);
                    cmd.Parameters.AddWithValue("@pass", usuarioLocal.Password);
                    cmd.Parameters.AddWithValue("@puestoid", usuarioLocal.puestoid);
                    //cmd.Parameters.AddWithValue("@rolid", usuarioLocal.rolid);
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;
                }
            }
            return updated;
        }

        public UsuarioLocal Login(string username, string password)
        {
            UsuarioLocal local = null;
            using (var conn = new SQLiteConnection(cadena))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand($"select * from usuarios WHERE username=@us AND password=@pass", conn))

                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@us", username);
                    cmd.Parameters.AddWithValue("@pass", password);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        local = new UsuarioLocal();
                        local.Id = int.Parse(rd["id"].ToString());
                        local.Nombre = rd["nombre"] as string;
                        local.Username = rd["username"] as string;
                        local.Password = rd["password"] as string;
                        local.puestoid = !string.IsNullOrEmpty(rd["puestoid"].ToString()) ? int.Parse(rd["puestoid"].ToString()) : 0;
                        local.Puesto = puestoLocalRepository.Get(local.puestoid).Descripcion;
                        local.Sync = int.Parse(rd["sync"].ToString());
                        local.Activo = int.Parse(rd["activo"].ToString());
                        //local.rolid =!string.IsNullOrEmpty(rd["rolid"].ToString()) ? int.Parse(rd["rolid"].ToString()):0; 
                    }
                }
                return local;
            }
        }

        public UsuarioLocal LoginByPassword(string password)
        {
            UsuarioLocal local = null;
            using (var conn = new SQLiteConnection(cadena))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand($"select * from usuarios WHERE password=@pass", conn))

                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@pass", password);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        local = new UsuarioLocal();
                        local.Id = int.Parse(rd["id"].ToString());
                        local.Nombre = rd["nombre"] as string;
                        local.Username = rd["username"] as string;
                        local.Password = rd["password"] as string;
                        local.puestoid = int.Parse(rd["puestoid"].ToString());
                        local.Puesto = puestoLocalRepository.Get(local.puestoid).Descripcion;
                        local.Sync = int.Parse(rd["sync"].ToString());
                        local.Activo = int.Parse(rd["activo"].ToString());
                        //local.rolid = !string.IsNullOrEmpty(rd["rolid"].ToString()) ? int.Parse(rd["rolid"].ToString()) : 0;

                    }
                }
                return local;
            }
        }
         
        public UsuarioLocal Get(int id)
        {
            UsuarioLocal user = null;
            using (var conn = new SQLiteConnection(cadena))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from usuarios WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        user = new UsuarioLocal();
                        user.Id = int.Parse(rd["id"].ToString());
                        user.Nombre = rd["nombre"] as string;
                        user.Username = rd["username"] as string;
                        user.Password = rd["password"] as string;
                        user.puestoid = int.Parse(rd["puestoid"].ToString());
                        user.Puesto = puestoLocalRepository.Get(user.puestoid).Descripcion;
                    }
                }
                return user;
            }
        }

        public List<UsuarioLocal> GetAllNoSync()
        {
            List<UsuarioLocal> users = new List<UsuarioLocal>();
            using (var conn = new SQLiteConnection(cadena))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand($"select * from usuarios where sync=0", conn))

                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        UsuarioLocal local = new UsuarioLocal();
                        local.Id = int.Parse(rd["id"].ToString());
                        local.Nombre = rd["nombre"] as string;
                        local.Username = rd["username"] as string;
                        local.Password = rd["password"] as string;
                        local.puestoid = !string.IsNullOrEmpty(rd["puestoid"].ToString()) ? int.Parse(rd["puestoid"].ToString()) : 0;
                        local.Puesto =local.puestoid >0 ? puestoLocalRepository.Get(local.puestoid).Descripcion: "";
                        local.Sync = int.Parse(rd["sync"].ToString());
                        local.Activo = int.Parse(rd["activo"].ToString());
                        users.Add(local);
                    }
                }
                return users;
            }
        }

        public void UpdateSync(List<int> id)
        {
            if (id.Count > 0)
            {

                using (var conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    foreach (var us in id)
                    {
                        using (var cmd = new SQLiteCommand($"update usuarios set sync=1 WHERE id=@id", conn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.AddWithValue("@id", us);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();

                }

            }

        }

        public UsuarioLocal GetLast()
        {
            UsuarioLocal user = null;
            using (var conn = new SQLiteConnection(cadena))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from usuarios order by id desc limit 1", conn))
                {
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        user = new UsuarioLocal();
                        user.Id = int.Parse(rd["id"].ToString());
                        user.Nombre = rd["nombre"] as string;
                        user.Username = rd["username"] as string;
                        user.Password = rd["password"] as string;
                        user.puestoid = int.Parse(rd["puestoid"].ToString());
                        user.Puesto = puestoLocalRepository.Get(user.puestoid).Descripcion;
                        user.Sync = int.Parse(rd["sync"].ToString());
                        user.Activo = int.Parse(rd["activo"].ToString());
                    }
                }
                return user;
            }
        }
    }
}
