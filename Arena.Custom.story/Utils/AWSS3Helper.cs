using System;
using System.Collections.Generic;
using System.Web;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Configuration;
using System.IO;

namespace Arena.Custom.Story.Utils
{
    /// <summary>
    /// Summary description for AWSS3Helper
    /// </summary>
    /// 

    public class AWSS3Helper
    {
        private string S3ACCESSKEY = ConfigurationManager.AppSettings["AWSACCESSKEY"];
        private string S3SECRETKEY = ConfigurationManager.AppSettings["AWSSECRETKEY"];

        public AWSS3Helper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Uri FetchFileUrl(string sObjectKey)
        {

            AmazonS3 client = AWSClientFactory.CreateAmazonS3Client(S3ACCESSKEY, S3SECRETKEY);
            string S3_KEY = ConfigurationManager.AppSettings["AWSS3KEY"];
            string BUCKET_NAME = ConfigurationManager.AppSettings["AWSBUCKET"];

            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest();
            request.WithBucketName(BUCKET_NAME);
            request.WithKey(sObjectKey);
            request.WithProtocol(Protocol.HTTP);
            request.WithExpires(DateTime.Now.AddMinutes(3));
            return (new Uri(client.GetPreSignedURL(request), UriKind.Absolute));
        }

        public byte[] FetchFile(string sObjectKey, string sVersionId)
        {
            AmazonS3 client = AWSClientFactory.CreateAmazonS3Client(S3ACCESSKEY, S3SECRETKEY);
            string BUCKET_NAME = ConfigurationManager.AppSettings["AWSBUCKET"];

            GetObjectRequest request = new GetObjectRequest();
            request.WithKey(sObjectKey);
            request.WithBucketName(BUCKET_NAME);

            if (sVersionId != "")
            {
                request.WithVersionId(sVersionId);
            }

            GetObjectResponse response = client.GetObject(request);

            byte[] buffer = new byte[response.ContentLength];

            int read;
            MemoryStream ms = new MemoryStream();
            while ((read = response.ResponseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return (ms.ToArray());

        }

        public bool SaveFile(string sFolder, string sObjectKey, byte[] fileContent, bool bMakePublic)
        {
            try
            {
                AmazonS3 client = AWSClientFactory.CreateAmazonS3Client(S3ACCESSKEY, S3SECRETKEY);
                Amazon.S3.Transfer.TransferUtility uploader = new Amazon.S3.Transfer.TransferUtility(S3ACCESSKEY, S3SECRETKEY);
                string BUCKET_NAME = ConfigurationManager.AppSettings["AWSBUCKET"];

                ListBucketsResponse response = client.ListBuckets();
                bool found = false;
                foreach (S3Bucket bucket in response.Buckets)
                {
                    if (bucket.BucketName == BUCKET_NAME)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    client.PutBucket(new PutBucketRequest().WithBucketName(BUCKET_NAME));
                }

                if (sFolder != "")
                {
                    sObjectKey = sFolder + "/" + sObjectKey;
                }

                System.IO.Stream stream = new System.IO.MemoryStream(fileContent);
                Amazon.S3.Transfer.TransferUtilityUploadRequest request = new Amazon.S3.Transfer.TransferUtilityUploadRequest();
                request.WithBucketName(BUCKET_NAME);
                request.WithKey(sObjectKey);
                request.WithInputStream(stream);
                request.WithTimeout(-1);
                if (bMakePublic)
                {
                    request.CannedACL = S3CannedACL.PublicRead;
                }
                uploader.Upload(request);
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }

        public string DeleteFile(string sFolder, string sObjectKey)
        {
            AmazonS3 client = AWSClientFactory.CreateAmazonS3Client(S3ACCESSKEY, S3SECRETKEY);
            string BUCKET_NAME = ConfigurationManager.AppSettings["AWSBUCKET"];

            if (sFolder != "")
            {
                sObjectKey = sFolder + "/" + sObjectKey;
            }

            DeleteObjectRequest deleteRequest = new Amazon.S3.Model.DeleteObjectRequest();
            deleteRequest.WithBucketName(BUCKET_NAME);
            deleteRequest.WithKey(sObjectKey);
            DeleteObjectResponse response = client.DeleteObject(deleteRequest);
            return (response.ResponseXml);
        }

        public ListVersionsResponse MssListFileVersions(string sObjectKey)
        {
            AmazonS3 client = AWSClientFactory.CreateAmazonS3Client(S3ACCESSKEY, S3SECRETKEY);
            string BUCKET_NAME = ConfigurationManager.AppSettings["AWSBUCKET"];

            return (client.ListVersions(new ListVersionsRequest().WithBucketName(BUCKET_NAME).WithKeyMarker(sObjectKey)));

        }

        public ListObjectsResponse ListingObjects(string Prefix, int MaxKeys)
        {

            AmazonS3 client = AWSClientFactory.CreateAmazonS3Client(S3ACCESSKEY, S3SECRETKEY);
            string BUCKET_NAME = ConfigurationManager.AppSettings["AWSBUCKET"];

            ListObjectsRequest request = new ListObjectsRequest
            {
                BucketName = BUCKET_NAME,
                Prefix = Prefix,
                MaxKeys = MaxKeys
            };
            ListObjectsResponse response = new ListObjectsResponse();
            try
            {

                response = client.ListObjects(request);

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    //Console.WriteLine("Check the provided AWS Credentials.");
                    //Console.WriteLine(
                    //"To sign up for service, go to http://aws.amazon.com/s3");
                }
                else
                {
                    //Console.WriteLine(
                    // "Error occurred. Message:'{0}' when listing objects",
                    // amazonS3Exception.Message);
                }
            }
            return (response);
        }

        public string GeneratePreSignedURL(String sFolder, String objectKey)
        {

            AmazonS3 client = AWSClientFactory.CreateAmazonS3Client(S3ACCESSKEY, S3SECRETKEY);
            string BUCKET_NAME = ConfigurationManager.AppSettings["AWSBUCKET"];
            string urlString;

            if (sFolder != "")
            {
                urlString = sFolder + "/";
            }
            else
            {
                urlString = "";
            }

            GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
            {
                BucketName = BUCKET_NAME,
                Key = objectKey,
                Expires = DateTime.Now.AddMinutes(5),
                Protocol = Amazon.S3.Model.Protocol.HTTP
            };

            try
            {
                urlString = client.GetPreSignedURL(request1);

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                    "To sign up for service, go to http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine(
                     "Error occurred. Message:'{0}' when listing objects",
                     amazonS3Exception.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return urlString;

        }
    }

}
