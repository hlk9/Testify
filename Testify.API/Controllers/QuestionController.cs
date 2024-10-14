using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;


namespace Testify.API.Controllers
{
    [Route("Question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        QuestionReposiroty _repoQuestion;
        QuestionLevelReposiroty _repoQuestionLevel;
        QuestionTypeReposiroty _repoQuestionType;

        public QuestionController()
        {
            _repoQuestion = new QuestionReposiroty();
            _repoQuestionLevel = new QuestionLevelReposiroty();
            _repoQuestionType = new QuestionTypeReposiroty();
        }

        [HttpGet("Get-All-Questions")]
        //[Authorize]
        public async Task<ActionResult<List<Question>>> GetlAllQuestions(string? keyWord, bool isActive)
        {
            var lstQuestion = await _repoQuestion.GetAllQuestions(keyWord, isActive);
            return Ok(lstQuestion);
        }

        [HttpGet("Get-Question-By-Id")]
        public async Task<ActionResult<Question>> GetQuestionById(int id)
        {
            var question = await _repoQuestion.GetQuestionById(id);
            return Ok(question);
        }

        [HttpPost("Create-Question")]
        public async Task<ActionResult<Question>> Create(Question question)
        {
            var createQuestion = await _repoQuestion.CreateQuestion(question);
            return Ok(createQuestion);
        }

        [HttpPut("Update-Question")]
        public async Task<ActionResult<Question>> Update(Question question)
        {
            var updateQuestion = await _repoQuestion.UpdateQuestion(question);
            return Ok(updateQuestion);
        }

        [HttpPut("Update-UploadFile-Question")]
        public async Task<ActionResult<Question>> UpdateUpload(Question question)
        {
            var updateQuestion = await _repoQuestion.UpdateDocumentPath(question);
            return Ok(updateQuestion);
        }

        [HttpPut("Update-Status")]
        public async Task<ActionResult<Question>> UpdateStatus(int questionId, byte status)
        {
            var updateStatus = await _repoQuestion.UpdateStatusQuestion(questionId, status);
            return Ok(updateStatus);
        }

        [HttpDelete("Delete-Question")]
        public async Task<ActionResult<Question>> Delete(int id)
        {
            var deleteQuestion = await _repoQuestion.DeleteQuestion(id);
            return Ok(deleteQuestion);
        }

        [HttpGet("Get-AnswerIsTrue-Point-Question")]
        public async Task<ActionResult<AnswerAndQuestion>> GetQuestionWithTrueAnswer(int questionId, int examDetailId)
        {
            var obj = await _repoQuestion.GetQuestionWithTrueAnswer(questionId, examDetailId);
            return Ok(obj);
        }

        [HttpGet("Export-Excel-Demo-Question")]
        public async Task<ActionResult> ExportExcel()
        {
            var lstQuestionLevel = await _repoQuestionLevel.GetAllLevels();
            var lstQuestionType = await _repoQuestionType.GetAllTypes();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Tạo một workbook mới bằng ClosedXML
            var package = new ExcelPackage();

            //sheet question template
            var worksheetQ = package.Workbook.Worksheets.Add("Câu Hỏi");

            worksheetQ.Cells.Style.Font.Name = "Times New Roman";
            System.Drawing.Color customColor = System.Drawing.Color.FromArgb(41, 166, 154); //màu chính của web
            System.Drawing.Color customColorYellow = System.Drawing.Color.Yellow;

            worksheetQ.Column(1).Width = 25;
            worksheetQ.Column(2).Width = 62;
            worksheetQ.Column(3).Width = 25;
            worksheetQ.Column(4).Width = 25;

            worksheetQ.Column(6).Width = 20;
            worksheetQ.Column(7).Width = 15;
            worksheetQ.Column(8).Width = 15;

            worksheetQ.Column(10).Width = 20;
            worksheetQ.Column(11).Width = 15;
            worksheetQ.Column(12).Width = 15;

            worksheetQ.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheetQ.Column(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheetQ.Column(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheetQ.Column(4).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheetQ.Column(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheetQ.Column(3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            worksheetQ.Cells[1, 1].Value = "Cột nối với sheet đáp án (kiểu số tăng dần bắt đầu từ 1)";
            worksheetQ.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetQ.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetQ.Cells[1, 1].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetQ.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetQ.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetQ.Cells[1, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetQ.Cells[1, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetQ.Cells[1, 1].Style.Font.Bold = true;
            worksheetQ.Cells[1, 1].Style.Font.Size = 12;

            worksheetQ.Cells[1, 2].Value = "Nội dung câu hỏi";
            worksheetQ.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetQ.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetQ.Cells[1, 2].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetQ.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetQ.Cells[1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetQ.Cells[1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetQ.Cells[1, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetQ.Cells[1, 2].Style.Font.Bold = true;
            worksheetQ.Cells[1, 2].Style.Font.Size = 12;

            worksheetQ.Cells[1, 3].Value = "Mức độ câu hỏi (Nhập mã ở bảng bên cạnh)";
            worksheetQ.Cells[1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetQ.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetQ.Cells[1, 3].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetQ.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetQ.Cells[1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetQ.Cells[1, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetQ.Cells[1, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetQ.Cells[1, 3].Style.Font.Bold = true;
            worksheetQ.Cells[1, 3].Style.Font.Size = 12;

            worksheetQ.Cells[1, 4].Value = "Loại câu hỏi (Chỉ dẫn bảng bên cạnh)";
            worksheetQ.Cells[1, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetQ.Cells[1, 4].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetQ.Cells[1, 4].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetQ.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetQ.Cells[1, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetQ.Cells[1, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetQ.Cells[1, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetQ.Cells[1, 4].Style.Font.Bold = true;
            worksheetQ.Cells[1, 4].Style.Font.Size = 12;

            //giá trị demo
            worksheetQ.Cells[2, 1].Value = "1";
            worksheetQ.Cells[2, 2].Value = "1 + 1 = ?";
            worksheetQ.Cells[2, 3].Value = "1";
            worksheetQ.Cells[2, 4].Value = "4";

            //bảng chỉ dẫn questionlevel
            worksheetQ.Cells[1, 6].Value = "Mã mức độ câu hỏi";
            worksheetQ.Cells[1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetQ.Cells[1, 6].Style.Fill.BackgroundColor.SetColor(customColorYellow);
            worksheetQ.Cells[1, 6].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetQ.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetQ.Cells[1, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetQ.Cells[1, 6].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetQ.Cells[1, 6].Style.Font.Bold = true;
            worksheetQ.Cells[1, 6].Style.Font.Size = 12;

            worksheetQ.Cells[1, 7].Value = "Tên mức độ câu hỏi";
            worksheetQ.Cells[1, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetQ.Cells[1, 7].Style.Fill.BackgroundColor.SetColor(customColorYellow);
            worksheetQ.Cells[1, 7].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetQ.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetQ.Cells[1, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetQ.Cells[1, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetQ.Cells[1, 7].Style.Font.Bold = true;
            worksheetQ.Cells[1, 7].Style.Font.Size = 12;

            //giá trị mặc định
            worksheetQ.Cells[2, 6].Value = "Nếu không có để rỗng";
            worksheetQ.Cells[2, 7].Value = "Không có";

            if (lstQuestionLevel != null || lstQuestionLevel?.Count != 0)
            {
                for (int i = 0; i < lstQuestionLevel.Count; i++)
                {
                    worksheetQ.Cells[i + 3, 6].Value = lstQuestionLevel[i].Id;
                    worksheetQ.Cells[i + 3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; 
                    worksheetQ.Cells[i + 3, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheetQ.Cells[i + 3, 7].Value = lstQuestionLevel[i].Name;
                    worksheetQ.Cells[i + 3, 7].Style.WrapText = true;

                }
            }

            //bảng chỉ dẫn questionType
            worksheetQ.Cells[1, 10].Value = "Mã loại câu hỏi";
            worksheetQ.Cells[1, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetQ.Cells[1, 10].Style.Fill.BackgroundColor.SetColor(customColorYellow);
            worksheetQ.Cells[1, 10].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetQ.Cells[1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetQ.Cells[1, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetQ.Cells[1, 10].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetQ.Cells[1, 10].Style.Font.Bold = true;
            worksheetQ.Cells[1, 10].Style.Font.Size = 12;

            worksheetQ.Cells[1, 11].Value = "Tên loại câu hỏi";
            worksheetQ.Cells[1, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetQ.Cells[1, 11].Style.Fill.BackgroundColor.SetColor(customColorYellow);
            worksheetQ.Cells[1, 11].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetQ.Cells[1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetQ.Cells[1, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetQ.Cells[1, 11].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetQ.Cells[1, 11].Style.Font.Bold = true;
            worksheetQ.Cells[1, 11].Style.Font.Size = 12;

            //giá trị mặc định
            worksheetQ.Cells[2, 10].Value = "Nếu không có để rỗng";
            worksheetQ.Cells[2, 11].Value = "Không có";
            if (lstQuestionType != null || lstQuestionType.Count != 0)
            {
                for (int i = 0; i < lstQuestionType.Count; i++)
                {
                    worksheetQ.Cells[i + 3, 10].Value = lstQuestionType[i].Id;
                    worksheetQ.Cells[i + 3, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheetQ.Cells[i + 3, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheetQ.Cells[i + 3, 11].Value = lstQuestionType[i].Name;
                    worksheetQ.Cells[i + 3, 11].Style.WrapText = true;
                }
            }

            //sheet answer template
            var worksheetA = package.Workbook.Worksheets.Add("Đáp án");

            worksheetA.Column(1).Width = 25;
            worksheetA.Column(2).Width = 62;
            worksheetA.Column(3).Width = 25;

            worksheetA.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheetA.Column(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheetA.Column(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheetA.Column(4).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheetA.Column(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheetA.Column(3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            worksheetA.Cells[1, 1].Value = "Cột nối với sheet câu hỏi (kiểu số tăng dần bắt đầu từ 1 - ít nhất 2 đáp án cho 1 câu hỏi)";
            worksheetA.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 1].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 1].Style.Font.Bold = true;
            worksheetA.Cells[1, 1].Style.Font.Size = 12;

            worksheetA.Cells[1, 2].Value = "Nội dung đáp án";
            worksheetA.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 2].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 2].Style.Font.Bold = true;
            worksheetA.Cells[1, 2].Style.Font.Size = 12;

            worksheetA.Cells[1, 3].Value = "Đúng/Sai (1/0)";
            worksheetA.Cells[1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheetA.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(customColor);
            worksheetA.Cells[1, 3].Style.WrapText = true; // Cho phép chữ tự xuống dòng
            worksheetA.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa theo chiều ngang
            worksheetA.Cells[1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // căn giữa theo chiều dọc
            worksheetA.Cells[1, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            worksheetA.Cells[1, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            worksheetA.Cells[1, 3].Style.Font.Bold = true;
            worksheetA.Cells[1, 3].Style.Font.Size = 12;

            worksheetA.Cells[2, 1].Value = "1";
            worksheetA.Cells[2, 2].Value = "2";
            worksheetA.Cells[2, 3].Value = "1";


            worksheetA.Cells[3, 1].Value = "1";
            worksheetA.Cells[3, 2].Value = "3";
            worksheetA.Cells[3, 3].Value = "0";

            worksheetA.Cells[4, 1].Value = "1";
            worksheetA.Cells[4, 2].Value = "4";
            worksheetA.Cells[4, 3].Value = "0";

            var excelByBytes = package.GetAsByteArray();

            return File(excelByBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Template_Question_LVT.xlsx");
        }
    }
}
