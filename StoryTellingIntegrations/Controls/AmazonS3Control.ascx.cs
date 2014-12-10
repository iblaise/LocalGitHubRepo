using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amazon.S3.Model;
using Arena.Custom.Story.Utils;
using Arena.Custom.Story.DataLayer;
using Arena.Custom.Story.Entity;
using System.Data;

public partial class Controls_AmazonS3Control : System.Web.UI.UserControl
{
    AWSS3Helper awsHelper = new AWSS3Helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //testing Arena DB Connection...
        Story story = new Story(1);


        DataTable storyTable = new StoryData().GetStoryList_DT("");

        

        ListObjectsResponse response = awsHelper.ListingObjects("MoreFiles/", 100);

        foreach (var item in response.S3Objects)
        {
            Response.Write("<a target='_blank' href='" + awsHelper.GeneratePreSignedURL("MoreFiles", item.Key) + "'>Link</a>");
            
        }

        lvDetail.DataSource = response.S3Objects;
        lvDetail.DataBind();
    }

    protected string GetAWSFiles(string key)
    {
        
        return awsHelper.GeneratePreSignedURL("MoreFiles", key);
    }

    protected void LinkButton1_ItemCommand(object sender, ListViewCommandEventArgs  e)
    {
        string key = e.CommandArgument.ToString();

        awsHelper.DeleteFile("", key);
    }

    protected void lvDetail_SelectedIndexChanged(object sender, EventArgs e)
    { 
        
    }
}