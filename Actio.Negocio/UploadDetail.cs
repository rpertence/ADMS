using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

public class UploadDetail
{
    public bool IsReady { get; set; }
    public int ContentLength { get; set; }
    public int UploadedLength { get; set; }
    public string FileName { get; set; }
}
