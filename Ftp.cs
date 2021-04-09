using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Security;
using System.Text;
using log4net;

namespace Food_Storage_Inventory
{
	public static class Ftp
	{
		private const string FILE_NAME = "Food Storage Inventory Location Data";
		private static readonly ILog logger = LogManager.GetLogger(typeof(Ftp));

		public static string DownloadFile()
		{
			try
			{
				FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://ftp.drivehq.com:21/{FILE_NAME}.json");
				request.Method = WebRequestMethods.Ftp.DownloadFile;

				request.Credentials = new NetworkCredential("dransom90", "60FuXSZWfq9U");
				request.KeepAlive = false;
				request.UseBinary = true;
				request.UsePassive = true;
				request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);

				FtpWebResponse response = (FtpWebResponse)request.GetResponse();

				Stream responseString = response.GetResponseStream();
				StreamReader reader = new StreamReader(responseString);
				return reader.ReadToEnd();
			}
			catch (NotSupportedException nse)
			{
				logger.Error(nse.Message);
				return string.Empty;
			}
			catch (ArgumentNullException ane)
			{
				logger.Error(ane.Message);
				return string.Empty;
			}
			catch (SecurityException se)
			{
				logger.Error(se.Message);
				return string.Empty;
			}
			catch (UriFormatException ufe)
			{
				logger.Error(ufe.Message);
				return string.Empty;
			}
			catch (Exception e)
			{
				logger.Error(e.Message);
				return string.Empty;
			}
		}

		public static bool UploadFile(string json)
		{
			try
			{
				FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://ftp.drivehq.com:21/{FILE_NAME}.json");
				request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
				request.Method = WebRequestMethods.Ftp.UploadFile;
				request.Credentials = new NetworkCredential("dransom90", "60FuXSZWfq9U");

				// Copy the contents of the file to the request stream
				byte[] fileContents = Encoding.UTF8.GetBytes(json);

				request.ContentLength = fileContents.Length;
				Stream requestStream = request.GetRequestStream();
				requestStream.Write(fileContents, 0, fileContents.Length);
				requestStream.Close();

				FtpWebResponse response = (FtpWebResponse)request.GetResponse();
				response.Close();

				return true;
			}
			catch (WebException e)
			{
				logger.Error(((FtpWebResponse)e.Response).StatusDescription);
				return false;
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				return false;
			}
		}
	}
}