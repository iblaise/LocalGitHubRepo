using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amazon.S3.Model;
using Arena.Custom.Story.Utils;

public partial class About : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
           

       
        //ListObjectsResponse response = awsHelper.ListingObjects("", 2);

        //foreach (var item in response.S3Objects)
        //{
        //    //awsHelper.
        //    Response.Write("<a target='_blank' href='" + awsHelper.GeneratePreSignedURL(item.Key) + "'>Link</a>");
        //}
    }

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
         AWSS3Helper awsHelper = new AWSS3Helper();
         Boolean fileOK = true;
            if (FileUpload1.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
               
            }

            if (fileOK)
            {
                try
                {
                    awsHelper.SaveFile("MoreFiles", Guid.NewGuid() + FileUpload1.FileName, FileUpload1.FileBytes, true); 
                    Label1.Text = "File uploaded!";
                }
                catch (Exception ex)
                {
                    Label1.Text = "File could not be uploaded.";
                }
            }
            else
            {
                Label1.Text = "Cannot accept files of this type.";
            }
        }
    }

