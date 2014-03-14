// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using KiiCorp.Cloud.Storage;

using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class ObjectBodyPage : BasePage, IPage
{
	private KiiObject obj;

	private Encoding enc = Encoding.GetEncoding("UTF-8");

	// Object content
	private Rect contentTextRect = new Rect(0, 64, 480, 64);
	private string content = "(Progress here)";

	// text area
	private Rect bodyTextAreaRect = new Rect(0, 128, 480, 256);

	// Upload
	private Rect uploadButtonRect = new Rect(0, 384, 320, 64);

	// Download
	private Rect downloadButtonRect = new Rect(320, 384, 320, 64);

	// Publish
	private Rect publishButtonRect = new Rect(0, 448, 320, 64);

	// PublishExpiresIn
	private Rect expireInTextRect = new Rect(0, 510, 160, 64);
	private Rect expireInSecondLabelRect = new Rect(160, 510, 160, 64);
	private Rect publishExpireInButtonRect = new Rect(320, 510, 320, 64);

	// PublishExpiresAt
	private Rect expireAtYearTextRect = new Rect(0, 574, 96, 64);
	private Rect expireAtYearLabelRect = new Rect(96, 574, 32, 64);
	private Rect expireAtMonthTextRect = new Rect(128, 574, 48, 64);
	private Rect expireAtMonthLabelRect = new Rect(176, 574, 32, 64);
	private Rect expireAtDayTextRect = new Rect(208, 574, 48, 64);

	private Rect expireAtHourTextRect = new Rect(256, 574, 48, 64);
	private Rect expireAtHourLabelRect = new Rect(304, 574, 32, 64);
	private Rect expireAtMinuteTextRect = new Rect(336, 574, 48, 64);
	private Rect expireAtMinuteLabelRect = new Rect(384, 574, 32, 64);
	private Rect expireAtSecondTextRect = new Rect(416, 574, 48, 64);

	private Rect publishExpireAtButtonRect = new Rect(480, 574, 320, 64);

	// Delete Body
	private Rect deleteBodyButtonRect = new Rect(0, 638, 320, 64);

	private string body;
	private string expireIn;
	private string expireAtYear;
	private string expireAtMonth;
	private string expireAtDay;
	private string expireAtHour;
	private string expireAtMinute;
	private string expireAtSecond;

	private bool buttonEnable = true;

	public ObjectBodyPage (MainCamera camera, KiiObject obj) : base(camera)
	{
		this.obj = obj;
		this.body = "";
		this.expireIn = "";
		DateTime now = DateTime.Now;
		this.expireAtYear = "" + now.Year;
		this.expireAtMonth = "" + now.Month;
		this.expireAtDay = "" + now.Day;
		this.expireAtHour = "" + now.Hour;
		this.expireAtMinute = "" + now.Minute;
		this.expireAtSecond = "" + now.Second;

	}

	#region IPage implementation

	public void OnGUI ()
	{
		GUI.Label(messageRect, message);
		GUI.TextField(contentTextRect, content, "TextField");
		expireIn = GUI.TextField(expireInTextRect, expireIn);
		GUI.Label(expireInSecondLabelRect, "seconds");

		expireAtYear = GUI.TextField(expireAtYearTextRect, expireAtYear);
		GUI.Label(expireAtYearLabelRect, "-");
		expireAtMonth = GUI.TextField(expireAtMonthTextRect, expireAtMonth);
		GUI.Label(expireAtMonthLabelRect, "-");
		expireAtDay = GUI.TextField(expireAtDayTextRect, expireAtDay);

		expireAtHour = GUI.TextField(expireAtHourTextRect, expireAtHour);
		GUI.Label(expireAtHourLabelRect, ":");
		expireAtMinute = GUI.TextField(expireAtMinuteTextRect, expireAtMinute);
		GUI.Label(expireAtMinuteLabelRect, ":");
		expireAtSecond = GUI.TextField(expireAtSecondTextRect, expireAtSecond);

		GUI.enabled = buttonEnable;
		bool backClicked = GUI.Button(backButtonRect, "<");
		this.body = GUI.TextArea(bodyTextAreaRect, this.body);
		bool uploadClicked = GUI.Button(uploadButtonRect, "Upload");
		bool downloadClicked = GUI.Button(downloadButtonRect, "Download");
		bool publishClicked = GUI.Button(publishButtonRect, "Publish");
		bool publishExpireInClicked = GUI.Button(publishExpireInButtonRect, "Publish ExpireIn");
		bool publishExpireAtClicked = GUI.Button(publishExpireAtButtonRect, "Publish ExpireAt");
		bool deleteBodyClicked = GUI.Button(deleteBodyButtonRect, "Delete body");
		GUI.enabled = true;

		if (backClicked)
		{
			PerformBack();
			return;
		}
		if (uploadClicked)
		{
			PerformUpload();
			return;
		}
		if (downloadClicked)
		{
			PerformDownload();
			return;
		}
		if (publishClicked)
		{
			PerformPublish();
			return;
		}
		if (publishExpireInClicked)
		{
			PerformPublishExpireIn();
			return;
		}
		if (publishExpireAtClicked)
		{
			PerformPublishExpireAt();
			return;
		}
		if (deleteBodyClicked)
		{
			PerformDeleteBody();
			return;
		}
	}
	
	void PerformDelete ()
	{
		message = "Deleting object...";
		ButtonEnabled = false;

		obj.Delete((KiiObject deletedObj, Exception e) =>
		{
			ButtonEnabled = true;
			if (e != null)
			{
				message = "Failed to delete " + e.ToString();
				return;
			}
			PerformBack();
		});
	}

	void PerformDeleteBody ()
	{
		message = "Deleting object body...";
		ButtonEnabled = false;
		
		obj.DeleteBody((KiiObject bodyDeletedObj, Exception e) =>
		           {
			ButtonEnabled = true;
			if (e != null)
			{
				message = "Failed to delete body : " + e.ToString();
				return;
			}
			message = "Delete body is succeeded.";
		});
	}

	void PerformUpload ()
	{
		message = "Uploading object body...";
		ButtonEnabled = false;

		byte[] data = enc.GetBytes(this.body);
		MemoryStream mem = new MemoryStream();
		mem.Write(data, 0, data.Length);
		mem.Seek(0, SeekOrigin.Begin);

		obj.UploadBody("text/plain", mem, (KiiObject obj2, Exception e) =>
		{
			ButtonEnabled = true;
			if (e != null)
			{
				message = "Failed to upload the body " + e.ToString();
				Debug.Log("body : " + (e as CloudException).Body);
				return;
			}
			message = "Upload body is succeeded.";
		},
		(KiiObject obj3, long doneByte, long totalByte) => 
		{
			this.content = String.Format("{0} / {1}", doneByte, totalByte);
			Debug.Log(this.content);
		});
	}

	void PerformDownload ()
	{
		message = "Downloading object body...";
		ButtonEnabled = false;

		MemoryStream mem = new MemoryStream(8192);

		obj.DownloadBody(mem, (KiiObject obj2, Stream s, Exception e) => {
			ButtonEnabled = true;
			if (e != null)
			{
				message = "Failed to download the body " + e.ToString();
				Debug.Log("body : " + (e as CloudException).Body);
				s.Close();
				return;
			}
			s.Seek(0, SeekOrigin.Begin);
			StreamReader sr = new StreamReader(s);
			this.body = sr.ReadToEnd();
			sr.Close();
			s.Close();
			message = "Download body is succeeded.";
		},
		(KiiObject obj3, long doneByte, long totalByte) => 
		{
			this.content = String.Format("{0} / {1}", doneByte, totalByte);
			Debug.Log(this.content);
		});
	}

	void PerformPublish ()
	{
		message = "Publishing object...";
		ButtonEnabled = false;

		obj.PublishBody((KiiObject obj2, string url, Exception e) =>
		{
			buttonEnable = true;
			if (e != null)
			{
				message = "Failed to publish " + e.ToString();
				Debug.Log (message);
				return;
			}
			message = "Publish body is succeeded.";
			this.content = String.Format("URL:{0}", url);
		});
	}

	void PerformPublishExpireIn ()
	{
		int time;
		try
		{
			time = int.Parse(expireIn);
		}
		catch (Exception e)
		{
			message = e.Message;
			return;
		}
		
		message = "Publishing object...";
		ButtonEnabled = false;
		
		obj.PublishBodyExpiresIn(time, (KiiObject obj2, string url, Exception e) =>
		{
			buttonEnable = true;
			if (e != null)
			{
				message = "Failed to publish " + e.ToString();
				Debug.Log (message);
				return;
			}
			message = "Publish body is succeeded.";
			this.content = String.Format("URL:{0}", url);
		});
	}

	void PerformPublishExpireAt ()
	{
		DateTime date;

		try
		{
			date = getExpireDate();
		}
		catch (Exception e)
		{
			message = e.Message;
			return;
		}

		message = "Publishing object...";
		ButtonEnabled = false;
		
		obj.PublishBodyExpiresAt(date, (KiiObject obj2, string url, Exception e) =>
		{
			buttonEnable = true;
			if (e != null)
			{
				message = "Failed to publish " + e.ToString();
				Debug.Log (message);
				return;
			}
			message = "Publish body is succeeded.";
			this.content = String.Format("URL:{0}", url);
		});
	}

	DateTime getExpireDate ()
	{
		int year = int.Parse(expireAtYear);
		int month = int.Parse(expireAtMonth);
		int day = int.Parse(expireAtDay);
		int hour = int.Parse(expireAtHour);
		int minute = int.Parse(expireAtMinute);
		int second = int.Parse(expireAtSecond);

		return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Local);
	}
	#endregion
}

