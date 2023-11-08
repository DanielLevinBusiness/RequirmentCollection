using ClosedXML.Excel;
using Dapper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using NPOI.HPSF;
using NPOI.HSSF.Record;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using NuGet.Protocol;
using OpsRequirmentCollectionWeb.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using static Azure.Core.HttpHeader;

namespace OpsRequirmentCollectionWeb.Controllers
{
    public class BudgetController : Controller
    {
        [HttpGet]
        //View for index
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        //[Authorize(Roles = "GER\\RPD ENG OPS, GER\\levindan, GER\\ad_levindan")]
        [Authorize(Roles = "GER\\Demand tool admins")]
        public ViewResult Admin()
        {
            return View();
        }

        [HttpGet]
        public ViewResult User()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Approved()
        {
            return View();
        }

        [HttpGet]
        public ViewResult ViewRequest()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetProjectNames(string DBname)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForProjectNames = connection.Query<string>
                    ("Select distinct ProjectName from " + DBname + " Order By ProjectName");

                return Json(queryForProjectNames.ToList());
            }
        }

        [HttpGet]
        public JsonResult SubmitStatusSet(bool IsClosed)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                string message = "";

                if (IsClosed)
                {
                    message = "Update submitStatus Set IsOpen = 'false' where ID = '1'";
                }
                else
                {
                    message = "Update submitStatus Set IsOpen = 'true' where ID = '1'";
                }

                var queryForStatus = connection.Query<string>
                        (message);

                return Json(queryForStatus.ToList());
            }
        }

        [HttpGet]
        public JsonResult SubmitStatusGet()
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForStatus = connection.Query<string>
                    ("Select IsOpen from submitStatus where ID = '1'");

                return Json(queryForStatus);
            }
        }

        [HttpGet]
        public JsonResult GetBoardNames(string ProjectName, string Setup)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForBoardNames = connection.Query<string>
                    ("Select distinct BoardName from EngBoards where ProjectName = '" + ProjectName
                    + "' AND Setup = '" + Setup + "' Order By BoardName");

                return Json(queryForBoardNames.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetFormfactor(string ProjectName)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForBoardNames = connection.Query<string>
                    ("Select distinct FormFactor from ProductBoards where ProjectName = '" + ProjectName + "' Order By FormFactor");

                return Json(queryForBoardNames.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetSkew(string ProjectName, string FormFactor)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForSubDescriptions = connection.Query<string>
                    ("Select distinct Skew from ProductBoards where ProjectName = '" + ProjectName
                    + "' AND FormFactor = '" + FormFactor + "' Order By Skew");

                return Json(queryForSubDescriptions.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetPackageNumber(string ProjectName)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForBoardNames = connection.Query<string>
                    ("Select distinct PackageNumber from SiPackage where ProjectName = '" + ProjectName + "' Order By PackageNumber");

                return Json(queryForBoardNames.ToList());
            }
        }

        private List<string> FilterQuery(IEnumerable<dynamic> queryForBoards) {

            var ResultString = string.Join(",", queryForBoards.ToArray());
            string[] SeperatedString = ResultString.Split(',');

            string sTemp = string.Empty;
            List<string> filteredString = new List<string>();
            var i = 0;

            foreach (var item in SeperatedString)
            {

                sTemp = item.Substring(item.IndexOf("=") + 1);

                sTemp = sTemp.Replace("'", "");

                sTemp = sTemp.Replace("}", "");

                sTemp = sTemp.Trim();

                if (sTemp == null || sTemp == "")
                    continue;

                if (!sTemp.Contains("{DapperRow"))
                {
                    filteredString.Add(sTemp);
                    i++;

                }
            }

            return filteredString;
        }

        [HttpGet]
        public List<string> GetEngBudgetItem(string DBname, string Requirment)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForBoards = connection.Query
                    ("Select ProjectName, Requestor, PartNumber, BoradName, GroupAndTeam, " +
                    "Quantity, QuantityOther, ItemJustification, Setup, SiStep" +
                    " from " + DBname + " " + Requirment + " Order By ProjectName");

                return FilterQuery(queryForBoards);
            }
        }

        [HttpGet]
        public List<string> GetSiBudgetItem(string DBname, string Requirment)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForBoards = connection.Query
                    ("Select ProjectName, Requestor, PartNumber, PackageNumber, GroupAndTeam, " +
                    "Quantity, QuantityOther, ItemJustification, MoldType, BlindOrTested, " +
                    "SiFLV, MiniSkew, Step from " + DBname + " " + Requirment + " Order By ProjectName");

                return FilterQuery(queryForBoards);
            }
        }

        [HttpGet]
        public List<string> GetProBudgetItem(string DBname, string Requirment)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForBoards = connection.Query
                    ("Select ProjectName, Requestor, PartNumber, FormFactor, " +
                    "GroupAndTeam, Quantity, QuantityOther, ItemJustification, " +
                    "Skew from " + DBname + " " + Requirment + " Order By ProjectName");

                return FilterQuery(queryForBoards);
            }
        }

        [HttpGet]
        public JsonResult GetSiFLV(string ProjectName, string PackageNumber)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForSiFLV = connection.Query<string>
                    ("Select distinct SiFLV from SiPackage where ProjectName = '" + ProjectName +
                    "' AND PackageNumber = '" + PackageNumber + "' Order By SiFLV");

                return Json(queryForSiFLV.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetSetup(string ProjectName)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForSubDescriptions = connection.Query<string>
                    ("Select distinct Setup from EngBoards where ProjectName = '" + ProjectName + "' Order By Setup");

                return Json(queryForSubDescriptions.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetEngPartNumber(string ProjectName, string BoardName, string Setup)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForSubDescriptions = connection.Query<string>
                    ("Select PartNumber from EngBoards where ProjectName = '" + ProjectName
                    + "' AND BoardName = '" + BoardName+ "' AND Setup = '" + Setup + "' Order By PartNumber");

                return Json(queryForSubDescriptions.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetProductPartNumber(string ProjectName, string FormFactor, string Skew)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForSubDescriptions = connection.Query<string>
                    ("Select PartNumber from ProductBoards where ProjectName = '" + ProjectName
                    + "' AND FormFactor = '" + FormFactor + "' AND Skew = '" + Skew + "' Order By Skew");

                return Json(queryForSubDescriptions.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetSiPartNumber(string ProjectName, string PackageNumber, string SiFLV)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForSubDescriptions = connection.Query<string>
                    ("Select PartNumber from SiPackage where ProjectName = '" + ProjectName
                    + "' AND PackageNumber = '" + PackageNumber + "' AND SiFLV = '" + SiFLV + "' Order By SiFLV");

                return Json(queryForSubDescriptions.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetRequestores()
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForRequestores = connection.Query
                    ("Select * from Requestors Order By FirstName");

                
                var ResultString = string.Join(",", queryForRequestores.ToArray());
                string[] SeperatedString = ResultString.Split(',');

                var sTemp = string.Empty;
                var Count = 2;

                foreach (var s in SeperatedString)
                {
                    if (s.Contains("FirstName") || s.Contains("LastName"))
                    {
                        sTemp += s.Substring(s.IndexOf('='));
                        sTemp = sTemp.Replace("'", "");
                        sTemp = sTemp.Replace("= ", "");

                        if (Count % 2 == 0)
                            sTemp += " ";

                        sTemp = sTemp.Replace("}", ",");
                        Count++;
                    }
                    else
                        continue;
                }

                sTemp = sTemp.Remove(sTemp.Length - 1);

                List<string> result = sTemp.Split(',').ToList();

                return Json(result);

            }
        }

        [HttpGet]
        public JsonResult GetGroupAndTeam()
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForGroupAndTeam = connection.Query<string>
                    ("Select * from GroupAndTeams Order By GroupName");

                return Json(queryForGroupAndTeam.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetEngBoardPrice(string ProjectName, string BoardName, string Setup)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForPrice = connection.Query<string>
                    ("Select PricePerUnit from EngBoards where ProjectName = '" + ProjectName
                    + "' AND Setup = '" + Setup + "' AND BoardName = '" + BoardName + "' Order By BoardName");

                return Json(queryForPrice.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetEngBoardComment(string ProjectName, string BoardName, string Setup)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForComment = connection.Query<string>
                    ("Select Comment from EngBoards where ProjectName = '" + ProjectName
                    + "' AND Setup = '" + Setup + "' AND BoardName = '" + BoardName + "' Order By BoardName");

                return Json(queryForComment.ToList());
            }
        }
        

        [HttpGet]
        public JsonResult GetSiComment(string ProjectName, string PackageNumber, string SiFLV)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForComment = connection.Query<string>
                    ("Select Comment from SiPackage where ProjectName = '" + ProjectName
                    + "' AND PackageNumber = '" + PackageNumber + "' AND SiFLV = '" + SiFLV + "' Order By PackageNumber");

                return Json(queryForComment.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetProductPrice(string ProjectName, string FormFactor, string Skew)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForPrice = connection.Query<string>
                    ("Select PricePerUnit from ProductBoards where ProjectName = '" + ProjectName
                    + "' AND FormFactor = '" + FormFactor + "' AND Skew = '" + Skew + "' Order By Skew");

                return Json(queryForPrice.ToList());
            }
        }

        [HttpGet]
        public JsonResult GetPackagePrice(string ProjectName, string PackageNumber, string SiFLV)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForPrice = connection.Query<string>
                    ("Select PricePerUnit from SiPackage where ProjectName = '" + ProjectName
                    + "' AND PackageNumber = '" + PackageNumber + "' AND SiFLV = '" +
                    SiFLV + "' Order By PricePerUnit");

                return Json(queryForPrice.ToList());
            }
        }

        [HttpGet]
        public JsonResult AddNewValueToDB(string newValue, string table, bool submit)
        {
            //remove ),( in the end in submit string
            if (submit == true)
                newValue = newValue.Remove(newValue.Length - 3);

            using (var connection = new DBModel("Mink").Connection)
            {
                try
                {
                    connection.Execute("Insert Into " + table + " values (" + newValue + ")") ;
                    return Json(true);
                }
                catch (Exception)
                {   
                    return Json(false);
                }
            }
        }

        [HttpGet]
        public List<string> GetApproved(string DBname, string Requirment)
        {
            using (var connection = new DBModel("Mink").Connection)
            {
                var queryForBoards = connection.Query
                    ("Select ApprovedQuantity, ApprovedQuantityOther, APO, ETA from " + DBname + " " + Requirment + " Order By ProjectName");

                return FilterQuery(queryForBoards);
            }
        }

        private void XclPrint(string[] SeperatedString, IXLWorksheet worksheet) {


            var rowIndex = 2;
            var row = worksheet.Row(rowIndex);
            var i = 1;

            foreach (var s in SeperatedString)
            {
                if (s.Contains('{'))
                {
                    continue;
                }
                else
                {
                    if (s.IndexOf('=') == -1)
                        continue;
                    var sTemp = s.Substring(s.IndexOf('='));
                    sTemp = sTemp.Replace("'", "");
                    sTemp = sTemp.Replace("=", "");
                    sTemp = sTemp.Trim();
                    if (s.Contains('}'))
                    {
                        sTemp = sTemp.Replace("}", "");
                        row.Cell(i).Value = sTemp;
                        //worksheet.AutoSizeColumn(i);
                        rowIndex++;
                        row = worksheet.Row(rowIndex);
                        i = 1;
                    }
                    else
                    {
                        row.Cell(i).Value = sTemp;
                        //worksheet.AutoSizeColumn(i);
                        i++;
                    }
                }
            }

        }

        public IActionResult ExportToXcl () {

            using (var connection = new DBModel("Mink").Connection)
            {
                try {
                    connection.Open();

                    var result = connection.Query("SELECT TOP (1000) [ProjectName]\r\n" +
                        "      ,[Requestor]\r\n" +
                        "      ,[PartNumber]\r\n" +
                        "      ,[BoradName]\r\n" +
                        "      ,[GroupAndTeam]\r\n" +
                        "      ,[Quantity]\r\n" +
                        "      ,[QuantityOther]\r\n" +
                        "      ,[ItemJustification]\r\n" +
                        "      ,[Setup]\r\n" +
                        "      ,[SiStep]\r\n" +
                        "  FROM [OpsRequirmentCollection].[dbo].[BudgetItemsENG]");


                    var ResultString = string.Join(",", result.ToArray());
                    string[] SeperatedString = ResultString.Split(',');
                    var TabNumber = 2;
                    var CellNumber = 1;


                    using (var workbook = new XLWorkbook())
                    {
                        //ENG boards excel print
                        IXLWorksheet worksheet =
                        workbook.Worksheets.Add("Eng Boards");

                        var rowIndex = 1;
                        var row = worksheet.Row(rowIndex);

                        row.Cell(CellNumber).Value = "Project Name";
                        row.Cell(++CellNumber).Value = "Requestor";
                        row.Cell(++CellNumber).Value = "Part Number";
                        row.Cell(++CellNumber).Value = "Board Name";
                        row.Cell(++CellNumber).Value = "Group And Team";
                        row.Cell(++CellNumber).Value = "Quantity for pre-BU";
                        row.Cell(++CellNumber).Value = "Quantity for post-BU";
                        row.Cell(++CellNumber).Value = "Item Justification";
                        row.Cell(++CellNumber).Value = "Setup";
                        row.Cell(++CellNumber).Value = "Si Step";

                        XclPrint(SeperatedString, worksheet);

                        result = connection.Query("SELECT TOP (1000) [ProjectName]\r\n" +
                        "      ,[Requestor]\r\n" +
                        "      ,[PartNumber]\r\n" +
                        "      ,[BoradName]\r\n" +
                        "      ,[GroupAndTeam]\r\n" +
                        "      ,[Quantity]\r\n" +
                        "      ,[QuantityOther]\r\n" +
                        "      ,[ItemJustification]\r\n" +
                        "      ,[Setup]\r\n" +
                        "      ,[SiStep]\r\n" +
                        "      ,[ApprovedQuantity]\r\n" +
                        "      ,[ApprovedQuantityOther]\r\n" +
                        "  FROM [OpsRequirmentCollection].[dbo].[BudgetItemsENGApp]");

                        ResultString = string.Join(",", result.ToArray());
                        SeperatedString = ResultString.Split(',');

                        workbook.Worksheets.Add("Eng Boards Approved");
                        worksheet = workbook.Worksheet(TabNumber);

                        rowIndex = 1;
                        row = worksheet.Row(rowIndex);
                        CellNumber = 1;

                        row.Cell(CellNumber).Value = "Project Name";
                        row.Cell(++CellNumber).Value = "Requestor";
                        row.Cell(++CellNumber).Value = "Part Number";
                        row.Cell(++CellNumber).Value = "BoradName";
                        row.Cell(++CellNumber).Value = "Group And Team";
                        row.Cell(++CellNumber).Value = "Quantity for pre-BU";
                        row.Cell(++CellNumber).Value = "Quantity for post-BU";
                        row.Cell(++CellNumber).Value = "Item Justification";
                        row.Cell(++CellNumber).Value = "Setup";
                        row.Cell(++CellNumber).Value = "SiStep";
                        row.Cell(++CellNumber).Value = "Approved Qty pre-BU";
                        row.Cell(++CellNumber).Value = "Approved Qty post-BU";

                        XclPrint(SeperatedString, worksheet);

                        //Product Boards excel print

                        result = connection.Query("SELECT TOP (1000) [ProjectName]\r\n" +
                        "      ,[Requestor]\r\n" +
                        "      ,[PartNumber]\r\n" +
                        "      ,[FormFactor]\r\n" +
                        "      ,[GroupAndTeam]\r\n" +
                        "      ,[Quantity]\r\n" +
                        "      ,[QuantityOther]\r\n" +
                        "      ,[ItemJustification]\r\n" +
                        "      ,[Skew]\r\n" +
                        "  FROM [OpsRequirmentCollection].[dbo].[BudgetItemsPRO]");


                        ResultString = string.Join(",", result.ToArray());
                        SeperatedString = ResultString.Split(',');

                        workbook.Worksheets.Add("Product Boards");
                        TabNumber++;
                        worksheet = workbook.Worksheet(TabNumber);

                        rowIndex = 1;
                        row = worksheet.Row(rowIndex);
                        CellNumber = 1;

                        row.Cell(CellNumber).Value = "Project Name";
                        row.Cell(++CellNumber).Value = "Requestor";
                        row.Cell(++CellNumber).Value = "Part Number";
                        row.Cell(++CellNumber).Value = "Form Factor";
                        row.Cell(++CellNumber).Value = "Group And Team";
                        row.Cell(++CellNumber).Value = "Quantity for pre-BU";
                        row.Cell(++CellNumber).Value = "Quantity for post-BU";
                        row.Cell(++CellNumber).Value = "Item Justification";
                        row.Cell(++CellNumber).Value = "Skew";

                        XclPrint(SeperatedString, worksheet);

                        result = connection.Query("SELECT TOP (1000) [ProjectName]\r\n" +
                        "      ,[Requestor]\r\n" +
                        "      ,[PartNumber]\r\n" +
                        "      ,[FormFactor]\r\n" +
                        "      ,[GroupAndTeam]\r\n" +
                        "      ,[Quantity]\r\n" +
                        "      ,[QuantityOther]\r\n" +
                        "      ,[ItemJustification]\r\n" +
                        "      ,[Skew]\r\n" +
                        "      ,[ApprovedQuantity]\r\n" +
                        "      ,[ApprovedQuantityOther]\r\n" +
                        "  FROM [OpsRequirmentCollection].[dbo].[BudgetItemsPROApp]");


                        ResultString = string.Join(",", result.ToArray());
                        SeperatedString = ResultString.Split(',');

                        workbook.Worksheets.Add("Product Boards Approved");
                        TabNumber++;
                        worksheet = workbook.Worksheet(TabNumber);

                        rowIndex = 1;
                        row = worksheet.Row(rowIndex);
                        CellNumber = 1;

                        row.Cell(CellNumber).Value = "Project Name";
                        row.Cell(++CellNumber).Value = "Requestor";
                        row.Cell(++CellNumber).Value = "Part Number";
                        row.Cell(++CellNumber).Value = "Form Factor";
                        row.Cell(++CellNumber).Value = "Group And Team";
                        row.Cell(++CellNumber).Value = "Quantity for pre-BU";
                        row.Cell(++CellNumber).Value = "Quantity for post-BU";
                        row.Cell(++CellNumber).Value = "Item Justification";
                        row.Cell(++CellNumber).Value = "Skew";
                        row.Cell(++CellNumber).Value = "Approved Qty pre-BU";
                        row.Cell(++CellNumber).Value = "Approved Qty post-BU";

                        XclPrint(SeperatedString, worksheet);


                        //Si Package excel print

                        result = connection.Query("SELECT TOP (1000) [ProjectName]\r\n" +
                        "      ,[Requestor]\r\n" +
                        "      ,[PartNumber]\r\n" +
                        "      ,[PackageNumber]\r\n" +
                        "      ,[GroupAndTeam]\r\n" +
                        "      ,[Quantity]\r\n" +
                        "      ,[QuantityOther]\r\n" +
                        "      ,[ItemJustification]\r\n" +
                        "      ,[MoldType]\r\n" +
                        "      ,[BlindOrTested]\r\n" +
                        "      ,[SiFLV]\r\n" +
                        "      ,[MiniSkew]\r\n" +
                        "      ,[Step]\r\n" +
                        "      ,[Recipient]\r\n" +
                        "      ,[DeliveryAddress]\r\n" +
                        "      ,[ETDDate]\r\n" +
                        "  FROM [OpsRequirmentCollection].[dbo].[BudgetItemsSI]");


                        ResultString = string.Join(",", result.ToArray());
                        SeperatedString = ResultString.Split(',');

                        workbook.Worksheets.Add("Si Packages");
                        TabNumber++;
                        worksheet = workbook.Worksheet(TabNumber);

                        rowIndex = 1;
                        row = worksheet.Row(rowIndex);
                        CellNumber = 1;

                        row.Cell(CellNumber).Value = "Project Name";
                        row.Cell(++CellNumber).Value = "Requestor";
                        row.Cell(++CellNumber).Value = "Part Number";
                        row.Cell(++CellNumber).Value = "Package Number";
                        row.Cell(++CellNumber).Value = "Group And Team";
                        row.Cell(++CellNumber).Value = "Quantity for pre-BU";
                        row.Cell(++CellNumber).Value = "Quantity for post-BU";
                        row.Cell(++CellNumber).Value = "Item Justification";
                        row.Cell(++CellNumber).Value = "Mold Type";
                        row.Cell(++CellNumber).Value = "Blind Or Tested";
                        row.Cell(++CellNumber).Value = "Si FLV";
                        row.Cell(++CellNumber).Value = "Mini Skew";
                        row.Cell(++CellNumber).Value = "Step";
                        row.Cell(++CellNumber).Value = "Recipient Name";
                        row.Cell(++CellNumber).Value = "Delivery Address";
                        row.Cell(++CellNumber).Value = "ETD Date";

                        XclPrint(SeperatedString, worksheet);

                        result = connection.Query("SELECT TOP (1000) [ProjectName]\r\n" +
                        "      ,[Requestor]\r\n" +
                        "      ,[PartNumber]\r\n" +
                        "      ,[PackageNumber]\r\n" +
                        "      ,[GroupAndTeam]\r\n" +
                        "      ,[Quantity]\r\n" +
                        "      ,[QuantityOther]\r\n" +
                        "      ,[ItemJustification]\r\n" +
                        "      ,[MoldType]\r\n" +
                        "      ,[BlindOrTested]\r\n" +
                        "      ,[SiFLV]\r\n" +
                        "      ,[MiniSkew]\r\n" +
                        "      ,[Step]\r\n" +
                        "      ,[ApprovedQuantity]\r\n" +
                        "      ,[ApprovedQuantityOther]\r\n" +
                        "  FROM [OpsRequirmentCollection].[dbo].[BudgetItemsSIApp]");


                        ResultString = string.Join(",", result.ToArray());
                        SeperatedString = ResultString.Split(',');

                        workbook.Worksheets.Add("Si Packages Approved");
                        TabNumber++;
                        worksheet = workbook.Worksheet(TabNumber);

                        rowIndex = 1;
                        row = worksheet.Row(rowIndex);
                        CellNumber = 1;

                        row.Cell(CellNumber).Value = "Project Name";
                        row.Cell(++CellNumber).Value = "Requestor";
                        row.Cell(++CellNumber).Value = "Part Number";
                        row.Cell(++CellNumber).Value = "Package Number";
                        row.Cell(++CellNumber).Value = "Group And Team";
                        row.Cell(++CellNumber).Value = "Quantity for pre-BU";
                        row.Cell(++CellNumber).Value = "Quantity for post-BU";
                        row.Cell(++CellNumber).Value = "Item Justification";
                        row.Cell(++CellNumber).Value = "Mold Type";
                        row.Cell(++CellNumber).Value = "Blind Or Tested";
                        row.Cell(++CellNumber).Value = "Si FLV";
                        row.Cell(++CellNumber).Value = "Mini Skew";
                        row.Cell(++CellNumber).Value = "Step";
                        row.Cell(++CellNumber).Value = "Approved Qty pre-BU";
                        row.Cell(++CellNumber).Value = "Approved Qty post-BU";

                        XclPrint(SeperatedString, worksheet);

                        //required using ClosedXML.Excel;  
                        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        string fileName = "RequirmentCollection.xlsx";

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, contentType, fileName);
                        }
                    }
                }

                catch (Exception e)
                {
                    return Json(e.Message);
                }

            }

        }

    }
}
