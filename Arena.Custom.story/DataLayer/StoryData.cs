using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arena.DataLib;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace Arena.Custom.Story.DataLayer
{
    public class StoryData : SqlData
    {

        /// <summary>
		/// Class constructor.
		/// </summary>
		public StoryData()
		{
		}

		/// <summary>
		/// Returns a <see cref="System.Data.SqlClient.SqlDataReader">SqlDataReader</see> object
		/// containing the Story with the ID specified
		/// </summary>
		/// <param name="storyId">Story ID</param>
		/// <returns><see cref="System.Data.SqlClient.SqlDataReader">SqlDataReader</see></returns>
		public SqlDataReader GetStoryByID(int storyId)
		{
			ArrayList lst = new ArrayList();

			lst.Add(new SqlParameter("@story_id", storyId));

			try
			{
                return this.ExecuteSqlDataReader("cust_kfs_sp_get_storyById", lst);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			finally
			{
				lst = null;
			}
		}

		/// <summary>
		/// deletes a Story record.
		/// </summary>
		public void DeleteStory(int libraryItemId)
		{
			ArrayList lst = new ArrayList();

            lst.Add(new SqlParameter("@story_id", libraryItemId));

			try
			{
                this.ExecuteNonQuery("cust_kfs_sp_del_story", lst);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			finally
			{
				lst = null;
			}

		}

		/// <summary>
		/// saves LibraryItem record
		/// </summary>
		/// <returns><see cref="System.Data.SqlClient.SqlDataReader">SqlDataReader</see></returns>
        public int SaveStory(int story_id, string title, int person_id, int category_luid, int organization_id, bool approved, string first_name,
            string last_name, string email, int approver, string source_key, string source_folder, string thumb_key, string thumb_folder, 
            string vimeo_key, bool allow_posting_online, bool allow_promo, string public_url, DateTime date_created, string created_by,
            DateTime date_modified, string modified_by)
		{
			ArrayList lst = new ArrayList();

            lst.Add(new SqlParameter("@story_id", story_id));
            lst.Add(new SqlParameter("@title", title));
            lst.Add(new SqlParameter("@person_id", person_id));
            lst.Add(new SqlParameter("@category_luid", category_luid));
            lst.Add(new SqlParameter("@organization_id", organization_id));
            lst.Add(new SqlParameter("@approved", approved));
            lst.Add(new SqlParameter("@first_name", first_name));
            lst.Add(new SqlParameter("@last_name", last_name));
            lst.Add(new SqlParameter("@email", email));
            lst.Add(new SqlParameter("@approver", approver));
            lst.Add(new SqlParameter("@source_key", source_key));
            lst.Add(new SqlParameter("@source_folder", source_folder));
            lst.Add(new SqlParameter("@thumb_key", thumb_key));
            lst.Add(new SqlParameter("@thumb_folder", thumb_folder));
            lst.Add(new SqlParameter("@vimeo_key", vimeo_key));
            lst.Add(new SqlParameter("@allow_posting_online", allow_posting_online));
            lst.Add(new SqlParameter("@allow_promo", allow_promo));
            lst.Add(new SqlParameter("@public_url", public_url));
            lst.Add(new SqlParameter("@date_created", date_created));
            lst.Add(new SqlParameter("@created_by", created_by));
            lst.Add(new SqlParameter("@date_modified", date_modified));
            lst.Add(new SqlParameter("@modified_by", modified_by));
            
            SqlParameter paramOut = new SqlParameter();
			paramOut.ParameterName = "@ID";
			paramOut.Direction = ParameterDirection.Output;
			paramOut.SqlDbType = SqlDbType.Int;
			lst.Add(paramOut);

			try
			{
                this.ExecuteNonQuery("cust_kfs_sp_save_story", lst);
				return (int)((SqlParameter)(lst[lst.Count - 1])).Value;
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2627) //Unique Key Violation
					return -1;
				else
					throw ex;
			}
			finally
			{
				lst = null;
			}
		}

		/// <summary>
		/// Returns a data table containing all Stories from the database With Search option
		/// </summary>
		/// <returns><see cref="System.Data.SqlClient.SqlDataReader">SqlDataReader</see></returns>
		public DataTable GetStoryList_DT(string searchParam)
		{
			ArrayList lst = new ArrayList();

            lst.Add(new SqlParameter("@search_param", searchParam));

			try
			{
                return this.ExecuteDataTable("cust_kfs_sp_get_story_list", lst);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			finally
			{
				lst = null;
			}
		}

		
    }
}
