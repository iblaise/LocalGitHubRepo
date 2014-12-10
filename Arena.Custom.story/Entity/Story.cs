using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arena.Core;
using System.Data.SqlClient;
using Arena.Custom.Story.DataLayer;

namespace Arena.Custom.Story.Entity
{
    /// <summary>
    /// Represents an item that can be checked out of a library.
    /// </summary>
    [Serializable]
    public class Story : ArenaObjectBase
    {
        #region Private Members

        private int _storyid = -1;
        private string _title = string.Empty;
        private int _personId = -1;
        private int _categoryId = -1;
        private int _organizationId = -1;
        private bool _approved = false;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _email = string.Empty;
        private int _approver = -1;
        private string _sourceKey = string.Empty;
        private string _sourceFolder = string.Empty;
        private string _thumbKey = string.Empty;
        private string _thumbFolder = string.Empty;
        private string _vimeoKey = string.Empty;
        private bool _allowPostingOnline = false;
        private bool _allowPromo = false;
        private string _publicURL = string.Empty;
        private DateTime _dateCreated = DateTime.MinValue;
        private string _createdBy = string.Empty;
        private DateTime _dateModified = DateTime.MinValue;
        private string _modifiedBy = string.Empty;


        #endregion

        #region Public Properties

        public int StoryId
        {
            get { return _storyid; }
            set { _storyid = value; }
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Person ID of submitter
        /// </summary>
        public int PersonId
        {
            get { return _personId; }
            set { _personId = value; }
        }

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        public int OrganizationId
        {
            get { return _organizationId; }
            set { _organizationId = value; }
        }

        public bool Approved
        {
            get { return _approved; }
            set { _approved = value; }
        }

        /// <summary>
        /// First Name of User
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


        public int Approver
        {
            get { return _approver; }
            set { _approver = value; }
        }

        public string SourceKey
        {
            get { return _sourceKey; }
            set { _sourceKey = value; }
        }

        public string SourceFolder
        {
            get { return _sourceFolder; }
            set { _sourceFolder = value; }
        }

        public string ThumbKey
        {
            get { return _thumbKey; }
            set { _thumbKey = value; }
        }

        public string ThumbFolder
        {
            get { return _thumbFolder; }
            set { _thumbFolder = value; }
        }

        public string VimeoKey
        {
            get { return _vimeoKey; }
            set { _vimeoKey = value; }
        }

        public bool AllowPostingOnline
        {
            get { return _allowPostingOnline; }
            set { _allowPostingOnline = value; }
        }

        public bool AllowPromo
        {
            get { return _allowPromo; }
            set { _allowPromo = value; }
        }

        public string PublicURL
        {
            get { return _publicURL; }
            set { _publicURL = value; }
        }

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        public DateTime DateModified
        {
            get { return _dateModified; }
            set { _dateModified = value; }
        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Saves the library item to the database
        /// </summary>
        /// <param name="userId">ID of person saving record</param>
        public void Save(string userId)
        {
            SaveStory(userId);
        }

        /// <summary>
        /// Deletes the specified library item from the database
        /// </summary>
        /// <param name="storyId"></param>
        static public void Delete(int storyId)
        {
            //new LibraryItemData().DeleteLibraryItem(libraryItemId);
            new StoryData().DeleteStory(storyId);
        }

        /// <summary>
        /// Deletes the current library item
        /// </summary>
        public void Delete()
        {

            //// delete record
            //LibraryItemData libraryitemData = new LibraryItemData();
            //libraryitemData.DeleteLibraryItem(_storyid);
            StoryData storyData = new StoryData();
            storyData.DeleteStory(_storyid);

            _storyid = -1;
        }

        #endregion

        #region Private Methods

        private void SaveStory(string userId)
        {

            _storyid = new StoryData().SaveStory(
                _storyid,
                _title,
                _personId,
                _categoryId,
                _organizationId,
                _approved,
                _firstName,
                _lastName,
                _email,
                _approver,
                _sourceKey,
                _sourceFolder,
                _thumbKey,
                _thumbFolder,
                _vimeoKey,
                _allowPostingOnline,
                _allowPromo,
                _publicURL,
                _dateCreated,
                _createdBy,
                _dateModified,
                _modifiedBy);
        }

        private void LoadStory(SqlDataReader reader)
        {
            if (!reader.IsDBNull(reader.GetOrdinal("story_id")))
                _storyid = (int)reader["story_id"];

            _title = reader["title"].ToString();

            if (!reader.IsDBNull(reader.GetOrdinal("person_id")))
                _personId = (int)reader["person_id"];

            if (!reader.IsDBNull(reader.GetOrdinal("category_luid")))
                _categoryId = (int)reader["category_luid"];

            if (!reader.IsDBNull(reader.GetOrdinal("organization_id")))
                _organizationId = (int)reader["organization_id"];

            if (!reader.IsDBNull(reader.GetOrdinal("approved")))
                _approved = (bool)reader["approved"];

            _firstName = reader["first_name"].ToString();

            _lastName = reader["last_name"].ToString();

            _email = reader["email"].ToString();

            if (!reader.IsDBNull(reader.GetOrdinal("approver")))
                _approver = (int)reader["approver"];

            _sourceKey = reader["source_key"].ToString();

            _sourceFolder = reader["source_folder"].ToString();

            _thumbKey = reader["thumb_key"].ToString();

            _thumbFolder = reader["thumb_folder"].ToString();

            _vimeoKey = reader["vimeo_key"].ToString();

            if (!reader.IsDBNull(reader.GetOrdinal("allow_posting_online")))
                _allowPostingOnline = (bool)reader["allow_posting_online"];

            if (!reader.IsDBNull(reader.GetOrdinal("allow_promo")))
                _allowPromo = (bool)reader["allow_promo"];

            _publicURL = reader["public_url"].ToString();

            if (!reader.IsDBNull(reader.GetOrdinal("date_created")))
                _dateCreated = (DateTime)reader["date_created"];

            if (!reader.IsDBNull(reader.GetOrdinal("date_modified")))
                _dateModified = (DateTime)reader["date_modified"];

            _createdBy = reader["created_by"].ToString();

            _modifiedBy = reader["modified_by"].ToString();

        }


        #endregion

        #region Constructors

        public Story()
        {

        }

        /// <summary>
        /// Loads the specified library item from the database.  If not found, libraryItemId will be -1.
        /// </summary>
        /// <param name="storyId"></param>
        public Story(int storyId)
        {
            SqlDataReader reader = new StoryData().GetStoryByID(storyId);
            if (reader.Read())
                LoadStory(reader);
            reader.Close();
        }

        /// <summary>
        /// Populates a library item with the current row from the reader that is passed in.
        /// </summary>
        /// <param name="reader"></param>
        public Story(SqlDataReader reader)
        {
            LoadStory(reader);
        }

        #endregion
    }
}
