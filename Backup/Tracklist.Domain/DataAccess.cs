using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Tracklist.Domain
{
    public class DataAccess
    {
        static string ConnectionString
        {
            get
            {
                return "server=dbs4.dailyrazor.com;Initial Catalog=soshimo;user=dbuser;password=5396aj;";
            }
        }

        static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }

        public static List<TrackInfo> GetAllTracks()
        {
            List<TrackInfo> trackList = new List<TrackInfo>();

            SqlConnection connection;
            using (connection = Connection)
            {
                try
                {
                   connection.Open();

                    SqlCommand cmd = new SqlCommand("Playlist.uspGetAllTracks", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TrackInfo info = new TrackInfo();

                        // hydrate info object
                        info.Album = SafeDbReader<string>.GetValue(reader, "album");
                        info.TrackId = SafeDbReader<long>.GetValue(reader, "trackid");
                        info.Url = SafeDbReader<string>.GetValue(reader, "url");
                        info.Artist = SafeDbReader<string>.GetValue(reader, "artist");
                        info.Genre = SafeDbReader<string>.GetValue(reader, "genre");
                        info.Hash = SafeDbReader<string>.GetValue(reader, "hash");
                        info.Song = SafeDbReader<string>.GetValue(reader, "title");

                        // add info object to collection
                        trackList.Add(info);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                }

                return trackList;
            }

        }


        public static void RemoveUserTrack(long trackid, Guid userid)
        {
            using (SqlConnection connection = Connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("Playlist.uspRemoveUserTrack", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userid", userid));
                    cmd.Parameters.Add(new SqlParameter("@trackId", trackid));
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static void AddTrack(TrackInfo track)
        {
            using (SqlConnection connection = Connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("Playlist.uspAddTrack", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@url", track.Url));
                    cmd.Parameters.Add(new SqlParameter("@description", ""));
                    cmd.Parameters.Add(new SqlParameter("@title", track.Song));
                    cmd.Parameters.Add(new SqlParameter("@artist", track.Artist));
                    cmd.Parameters.Add(new SqlParameter("@album", track.Album));
                    cmd.Parameters.Add(new SqlParameter("@genre", track.Genre));
                    cmd.Parameters.Add(new SqlParameter("@hash", track.Hash));
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static void AddUserTrack(long trackid, Guid userid)
        {
            using (SqlConnection connection = Connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("Playlist.uspAddUserTrack", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@trackId", trackid));
                    cmd.Parameters.Add(new SqlParameter("@userid", userid));
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static List<TrackInfo> GetTracklist(Guid userid)
        {
            List<TrackInfo> trackList = new List<TrackInfo>();

            SqlConnection connection;
            using (connection = Connection)
            {
                try
                {
                    connection.Open();

                    string sql = "Playlist.uspGetPlaylist";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userid", userid));
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TrackInfo info = new TrackInfo();

                        // hydrate the object
                        info.Album = SafeDbReader<string>.GetValue(reader, "album");
                        info.TrackId = SafeDbReader<long>.GetValue(reader, "trackid");
                        info.Url = SafeDbReader<string>.GetValue(reader, "url");
                        info.Artist = SafeDbReader<string>.GetValue(reader, "artist");
                        info.Genre = SafeDbReader<string>.GetValue(reader, "genre");
                        info.Hash = SafeDbReader<string>.GetValue(reader, "hash");
                        info.Song = SafeDbReader<string>.GetValue(reader, "title");

                        // and add to collection
                        trackList.Add(info);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                }

                return trackList;
            }

        }

        public static TrackInfo GetTrackInfo(long trackId)
        {
            TrackInfo info = null;

            SqlConnection connection;
            using (connection = Connection)
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("Playlist.uspGetTrackInfo", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@trackid", trackId));

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // hydrate here
                        info = new TrackInfo();
                        info.Album = SafeDbReader<string>.GetValue(reader, "album");
                        info.TrackId = SafeDbReader<long>.GetValue(reader, "trackid");
                        info.Url = SafeDbReader<string>.GetValue(reader, "url");
                        info.Artist = SafeDbReader<string>.GetValue(reader, "artist");
                        info.Genre = SafeDbReader<string>.GetValue(reader, "genre");
                        info.Hash = SafeDbReader<string>.GetValue(reader, "hash");
                        info.Song = SafeDbReader<string>.GetValue(reader, "title");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                }

                return info;
            }
        }

        public static void RegisterUser(Guid userId, string email, string salt, string password, string hint, string question)
        {
            SqlConnection connection;
            using (connection = Connection)
            {
                try
                {
                    connection.Open();
                    string sql = "Playlist.uspRegisterUser";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userId", userId));
                    cmd.Parameters.Add(new SqlParameter("@email", email));
                    cmd.Parameters.Add(new SqlParameter("@salt", salt));
                    cmd.Parameters.Add(new SqlParameter("@password", password));
                    cmd.Parameters.Add(new SqlParameter("@hint", hint));
                    cmd.Parameters.Add(new SqlParameter("@question", question));
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
            }
        }


        public static void UpdateUserInfo(Guid userId, UserInfo info)
        {
            using (SqlConnection connection = Connection)
            {
                try
                {
                    connection.Open();

                    string sql = "Playlist.uspUpdateUserInfo";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userId", userId));
                    cmd.Parameters.Add(new SqlParameter("@email", info.Email));
                    cmd.Parameters.Add(new SqlParameter("@hint", info.Hint));
                    cmd.Parameters.Add(new SqlParameter("@password", info.Password));
                    cmd.Parameters.Add(new SqlParameter("@salt", info.Salt));
                    cmd.Parameters.Add(new SqlParameter("@question", info.Question));

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }

            }
        }

        public static UserInfo GetUserInfoByUserId(Guid userId)
        {
            UserInfo returnValue = null;
            using (SqlConnection connection = Connection)
            {
                try
                {
                    connection.Open();

                    string sql = "Playlist.uspGetUserInfoByUserId";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userId", userId));

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        returnValue = new UserInfo();
                        returnValue.UserId = SafeDbReader<Guid>.GetValue(reader, "userid");
                        returnValue.Email = SafeDbReader<string>.GetValue(reader, "emailaddress");
                        returnValue.Hint = SafeDbReader<string>.GetValue(reader, "hint");
                        returnValue.Password = SafeDbReader<string>.GetValue(reader, "password");
                        returnValue.Salt = SafeDbReader<string>.GetValue(reader, "salt");
                        returnValue.Question = SafeDbReader<string>.GetValue(reader, "question");
                    }
                }
                catch (Exception ex)
                {
                }

                return returnValue;
            }
        }

        public static UserInfo GetUserInfoByEmail(string email)
        {
            UserInfo returnValue = null;
            using (SqlConnection connection = Connection)
            {
                try
                {
                    connection.Open();

                    string sql = "Playlist.uspGetUserInfoByEmail";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@email", email));

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        returnValue = new UserInfo();
                        returnValue.UserId = SafeDbReader<Guid>.GetValue(reader, "userid");
                        returnValue.Email = SafeDbReader<string>.GetValue(reader, "emailaddress");
                        returnValue.Hint = SafeDbReader<string>.GetValue(reader, "hint");
                        returnValue.Password = SafeDbReader<string>.GetValue(reader, "password");
                        returnValue.Salt = SafeDbReader<string>.GetValue(reader, "salt");
                        returnValue.Question = SafeDbReader<string>.GetValue(reader, "question");
                    }
                }
                catch (Exception ex)
                {
                }

                return returnValue;
            }
        }


        public static void AddPlaylistEntry(Guid userId, long trackId)
        {
            using (SqlConnection connection = Connection)
            {
                try
                {
                    connection.Open();

                    string sql = "Playlist.uspAddUserTrack";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userId", userId));
                    cmd.Parameters.Add(new SqlParameter("@trackId", trackId));

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }

            }
        }
    }
}
