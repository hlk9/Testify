using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
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
        AnswerReposiroty _repoAnswer;
        SubjectRepository _repoSubject;

        public QuestionController()
        {
            _repoQuestion = new QuestionReposiroty();
            _repoQuestionLevel = new QuestionLevelReposiroty();
            _repoQuestionType = new QuestionTypeReposiroty();
            _repoAnswer = new AnswerReposiroty();
            _repoSubject = new SubjectRepository();
        }

        [HttpGet("Get-All-Questions")]
        //[Authorize]
        public async Task<ActionResult<List<Question>>> GetlAllQuestions(string? keyWord, Guid? userId)
        {
            string decodedContent = "";
            if(!string.IsNullOrEmpty(keyWord) || !string.IsNullOrWhiteSpace(keyWord))
            {
                decodedContent = Uri.UnescapeDataString(keyWord);
            }
            var lstQuestion = await _repoQuestion.GetAllQuestions(decodedContent, userId);
            return Ok(lstQuestion);
        }

        [HttpGet("Get-Question-By-Id")]
        public async Task<ActionResult<Question>> GetQuestionById(int id)
        {
            var question = await _repoQuestion.GetQuestionById(id);
            return Ok(question);
        }

        [HttpGet("Check-Validate")]
        public async Task<ActionResult<bool>> CheckValidate(string content, int questionTypeId, int subjectId, int? questionId)
        {
            string decodedContent = Uri.UnescapeDataString(content);
            var hasQuestion = await _repoQuestion.CheckValidate(decodedContent, questionTypeId, subjectId, questionId);
            return Ok(hasQuestion);
        }

        [HttpGet("Check-Update")]
        public async Task<ActionResult<bool>> CheckUpdate(int questionId)
        {
            var hasQuestion = await _repoQuestion.CheckUpdateIsExamSchedule(questionId);
            return Ok(hasQuestion);
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
        public async Task<ActionResult> Delete(int id)
        {
            var deleteQuestion = await _repoQuestion.DeleteQuestion(id);

            if (deleteQuestion.Success)
            {
                return NoContent();
            }

            return BadRequest(new
            {
                ErrorCode = deleteQuestion.ErrorCode,
                Message = deleteQuestion.Message
            });
        }

        [HttpGet("Get-AnswerIsTrue-Point-Question")]
        public async Task<ActionResult<AnswerAndQuestion>> GetQuestionWithTrueAnswer(int questionId, int examDetailId)
        {
            var obj = await _repoQuestion.GetQuestionWithTrueAnswer(questionId, examDetailId);
            return Ok(obj);
        }

        [HttpGet("Export-Excel-Template-Question")]
        public async Task<ActionResult> ExportExcel(string? textSearch)
        {
            var lstQuestionLevel = await _repoQuestionLevel.GetAllLevels(textSearch);
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
            worksheetQ.Cells[2, 1].Value = 1;
            worksheetQ.Cells[2, 2].Value = "1 + 1 = ?";
            worksheetQ.Cells[2, 3].Value = 1;
            worksheetQ.Cells[2, 4].Value = 2;

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

            worksheetA.Cells[2, 1].Value = 1;
            worksheetA.Cells[2, 2].Value = 2;
            worksheetA.Cells[2, 3].Value = 1;


            worksheetA.Cells[3, 1].Value = 1;
            worksheetA.Cells[3, 2].Value = 3;
            worksheetA.Cells[3, 3].Value = 0;

            worksheetA.Cells[4, 1].Value = 1;
            worksheetA.Cells[4, 2].Value = 4;
            worksheetA.Cells[4, 3].Value = 0;

            var excelByBytes = package.GetAsByteArray();

            return File(excelByBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Template_Question_LVT.xlsx");
        }

        [HttpPost("Import-Excel-Question")]
        public async Task<ActionResult<int>> UploadFile(IFormFile file, [FromForm] int subjectId, [FromForm] Guid? userId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Chưa chọn file import");
            }

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var package = new ExcelPackage(stream);

            var lstQuestionLevel = await _repoQuestionLevel.GetAllLevels("");
            var lstQuestionType = await _repoQuestionType.GetAllTypes();
            var lstQuestion = await _repoQuestion.GetAllQuestions("", null);

            var lstQuestionTemp = new List<QuestionInExcel>();

            var questionFailCount = 0;

            var worksheetsQ = package.Workbook.Worksheets[0];
            var worksheetsA = package.Workbook.Worksheets[1];

            for (int rowQ = 2; rowQ <= worksheetsQ.Dimension.Rows; rowQ++)
            {
                if (worksheetsQ.Cells[rowQ, 1].Value == null ||
                    worksheetsQ.Cells[rowQ, 2].Value == null ||
                    worksheetsQ.Cells[rowQ, 4].Value == null || 
                    string.IsNullOrEmpty(worksheetsQ.Cells[rowQ, 1].Value.ToString()) || 
                    string.IsNullOrWhiteSpace(worksheetsQ.Cells[rowQ, 1].Value.ToString()) ||
                    string.IsNullOrEmpty(worksheetsQ.Cells[rowQ, 2].Value.ToString()) ||
                    string.IsNullOrWhiteSpace(worksheetsQ.Cells[rowQ, 2].Value.ToString()) ||
                    string.IsNullOrEmpty(worksheetsQ.Cells[rowQ, 4].Value.ToString()) ||
                    string.IsNullOrWhiteSpace(worksheetsQ.Cells[rowQ, 4].Value.ToString()))
                {
                    questionFailCount++;
                    continue;
                }
                else if ((lstQuestionTemp.Any(x => x.Content.Trim().ToLower().Equals(worksheetsQ.Cells[rowQ, 2].Value.ToString().Trim().ToLower()) && x.QuestionTypeId == Convert.ToInt32(worksheetsQ.Cells[rowQ, 4].Value)) || lstQuestion.Any(x => x.Content.Trim().ToLower().Equals(worksheetsQ.Cells[rowQ, 2].Value.ToString().Trim().ToLower()) && x.QuestionTypeId == Convert.ToInt32(worksheetsQ.Cells[rowQ, 4].Value) && x.SubjectId == subjectId)))
                {
                    questionFailCount++;
                    continue;
                }
                else if (worksheetsQ.Cells[rowQ, 3].Value != null && !lstQuestionLevel.Any(x => x.Id == Convert.ToInt32(worksheetsQ.Cells[rowQ, 3].Value.ToString().Trim())))
                {
                    questionFailCount++;
                    continue;
                }
                else if (!lstQuestionType.Any(x => x.Id == Convert.ToInt32(worksheetsQ.Cells[rowQ, 4].Value.ToString().Trim())))
                {
                    questionFailCount++;
                    continue;
                }

                List<Answer> lstAnswer = new List<Answer>();
                int countAnswer = 0;
                bool isValid = true;

                for (int rowA = 2; rowA <= worksheetsA.Dimension.Rows; rowA++)
                {
                    if (worksheetsA.Cells[rowA, 1].Value == null)
                    {
                        continue;
                    }

                    if (worksheetsQ.Cells[rowQ, 1].Value.ToString().Trim() == worksheetsA.Cells[rowA, 1].Value.ToString().Trim())
                    {
                        countAnswer++;
                        if (worksheetsA.Cells[rowA, 2].Value == null || 
                            worksheetsA.Cells[rowA, 3].Value == null || 
                            string.IsNullOrEmpty(worksheetsA.Cells[rowA, 2].Value.ToString()) ||
                            string.IsNullOrWhiteSpace(worksheetsA.Cells[rowA, 2].Value.ToString()) ||
                            string.IsNullOrEmpty(worksheetsA.Cells[rowA, 3].Value.ToString()) ||
                            string.IsNullOrWhiteSpace(worksheetsA.Cells[rowA, 3].Value.ToString()))
                        {
                            isValid = false;
                            break;
                        }

                        Answer answer = new Answer();
                        answer.Content = worksheetsA.Cells[rowA, 2].Value.ToString().Trim();
                        answer.IsCorrect = worksheetsA.Cells[rowA, 3].Value.ToString().Trim() == "1" ? true : false;
                        lstAnswer.Add(answer);
                    }
                }

                if (isValid && lstAnswer.Count == countAnswer)
                {
                    int countAnswerCorrect = lstAnswer.Count(x => x.IsCorrect);

                    if (countAnswerCorrect <= 0)
                    {
                        questionFailCount++;
                        continue;
                    }
                    else if ((Convert.ToInt32(worksheetsQ.Cells[rowQ, 4].Value.ToString().Trim()) == 2 || Convert.ToInt32(worksheetsQ.Cells[rowQ, 4].Value.ToString().Trim()) == 3) && lstAnswer.Count < 2)
                    {
                        questionFailCount++;
                        continue;
                    }
                    else if (Convert.ToInt32(worksheetsQ.Cells[rowQ, 4].Value) == 1 && lstAnswer.Count != 2)
                    {
                        questionFailCount++;
                        continue;
                    }
                    else if ((Convert.ToInt32(worksheetsQ.Cells[rowQ, 4].Value.ToString().Trim()) == 1 || Convert.ToInt32(worksheetsQ.Cells[rowQ, 4].Value.ToString().Trim()) == 2) && countAnswerCorrect > 1)
                    {
                        questionFailCount++;
                        continue;
                    }

                    QuestionInExcel QnA = new QuestionInExcel();
                    QnA.Content = worksheetsQ.Cells[rowQ, 2].Value.ToString().Trim();
                    QnA.QuestionTypeId = Convert.ToInt32(worksheetsQ.Cells[rowQ, 4].Value.ToString().Trim());
                    QnA.Answers = lstAnswer;
                    lstQuestionTemp.Add(QnA);

                    var question = new Question
                    {
                        Content = worksheetsQ.Cells[rowQ, 2].Value.ToString().Trim(),
                        QuestionLevelId = Convert.ToInt32(worksheetsQ.Cells[rowQ, 3].Value.ToString().Trim()) == 0 ? null : Convert.ToInt32(worksheetsQ.Cells[rowQ, 3].Value.ToString().Trim()),
                        QuestionTypeId = Convert.ToInt32(worksheetsQ.Cells[rowQ, 4].Value.ToString().Trim()),
                        CreatedDate = DateTime.Now,
                        Status = 1,
                        SubjectId = subjectId,
                        CreatedBy = userId
                    };

                    var successAddQuestion = await _repoQuestion.CreateQuestion(question);

                    if (successAddQuestion != null)
                    {
                        foreach (var answer in lstAnswer)
                        {
                            answer.QuestionId = successAddQuestion.Id;
                            answer.Status = 1;
                            await _repoAnswer.CreateAnswer(answer);
                        }
                    }

                }
                else
                {
                    questionFailCount++;
                }
            }
            lstQuestionTemp.Clear();
            return questionFailCount;
        }

        [HttpGet("Export-Question-By-SubjectId")]
        public async Task<ActionResult> ExportQuestionBySubjectId(int subjectId, bool isAnswer)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var package = new ExcelPackage();
            if (subjectId != 0 && subjectId != null)
            {
                var objSubject = await _repoSubject.GetSubjectById(subjectId);
                var lstQnA = await _repoQuestion.Check(subjectId);

                if (lstQnA != null && lstQnA.Count > 0)
                {
                    var countAnswerMax = lstQnA.OrderByDescending(x => x.Answers.Count()).FirstOrDefault().Answers.Count();
                    var worksheetQ = package.Workbook.Worksheets.Add("Câu Hỏi và đáp án");

                    worksheetQ.Cells.Style.Font.Name = "Times New Roman";
                    System.Drawing.Color customColor = System.Drawing.Color.FromArgb(41, 166, 154); //màu chính của web
                    System.Drawing.Color answerCorrect = System.Drawing.Color.FromArgb(112, 173, 71);

                    worksheetQ.Column(1).Width = 6;
                    worksheetQ.Column(2).Width = 62;
                    worksheetQ.Column(3).Width = 25;

                    worksheetQ.Row(1).Height = 30;

                    worksheetQ.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheetQ.Column(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheetQ.Column(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheetQ.Column(3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheetQ.Cells[1, 1].Value = "Stt";
                    worksheetQ.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheetQ.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(customColor);
                    worksheetQ.Cells[1, 1].Style.WrapText = true;
                    worksheetQ.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheetQ.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheetQ.Cells[1, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    worksheetQ.Cells[1, 1].Style.Font.Bold = true;
                    worksheetQ.Cells[1, 1].Style.Font.Size = 12;

                    worksheetQ.Cells[1, 2].Value = "Nội dung câu hỏi";
                    worksheetQ.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheetQ.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(customColor);
                    worksheetQ.Cells[1, 2].Style.WrapText = true;
                    worksheetQ.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheetQ.Cells[1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheetQ.Cells[1, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    worksheetQ.Cells[1, 2].Style.Font.Bold = true;
                    worksheetQ.Cells[1, 2].Style.Font.Size = 12;

                    worksheetQ.Cells[1, 3].Value = "Môn";
                    worksheetQ.Cells[1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheetQ.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(customColor);
                    worksheetQ.Cells[1, 3].Style.WrapText = true;
                    worksheetQ.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheetQ.Cells[1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheetQ.Cells[1, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    worksheetQ.Cells[1, 3].Style.Font.Bold = true;
                    worksheetQ.Cells[1, 3].Style.Font.Size = 12;

                    if (isAnswer)
                    {
                        for (int i = 1; i <= countAnswerMax; i++)
                        {
                            worksheetQ.Column(i + 3).Width = 20;
                            worksheetQ.Column(i + 3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheetQ.Column(i + 3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            worksheetQ.Cells[1, i + 3].Value = $"Đáp án {i}";
                            worksheetQ.Cells[1, i + 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheetQ.Cells[1, i + 3].Style.Fill.BackgroundColor.SetColor(customColor);
                            worksheetQ.Cells[1, i + 3].Style.WrapText = true;
                            worksheetQ.Cells[1, i + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheetQ.Cells[1, i + 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            worksheetQ.Cells[1, i + 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
                            worksheetQ.Cells[1, i + 3].Style.Font.Bold = true;
                            worksheetQ.Cells[1, i + 3].Style.Font.Size = 12;
                        }
                    }

                    for (int i = 0; i < lstQnA.Count; i++)
                    {
                        worksheetQ.Cells[i + 2, 1].Value = (i + 1).ToString();
                        worksheetQ.Cells[i + 2, 2].Value = lstQnA[i].Content.ToString();
                        worksheetQ.Cells[i + 2, 3].Value = objSubject.Name.ToString();
                        worksheetQ.Cells[i + 2, 2].Style.WrapText = true;
                        worksheetQ.Cells[i + 2, 3].Style.WrapText = true;

                        if (isAnswer)
                        {
                            for (int j = 0; j < countAnswerMax; j++)
                            {
                                if (j < lstQnA[i].Answers.Count())
                                {
                                    worksheetQ.Cells[i + 2, j + 4].Value = lstQnA[i].Answers[j].Content.ToString();
                                    if (lstQnA[i].Answers[j].IsCorrect)
                                    {
                                        worksheetQ.Cells[i + 2, j + 4].Style.Font.Bold = true;
                                        worksheetQ.Cells[i + 2, j + 4].Style.Font.Color.SetColor(answerCorrect);
                                    }
                                }
                                else
                                {
                                    worksheetQ.Cells[i + 2, j + 4].Value = "NaN";
                                    worksheetQ.Cells[i + 2, j + 4].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#8e8e8e"));
                                }
                            }
                        }
                    }

                    var excelByBytes = package.GetAsByteArray();

                    return File(excelByBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"List_Question.xlsx");
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("Get-Question-By-Id_Sub-And-Level")]
        //[Authorize]
        public async Task<ActionResult<List<QuestionInExam>>> GetAllQuestionById_sub_andlevel(int id_sub, int id_level)
        {
            var lstQuestion = await _repoQuestion.GetQuestionBySubjectIdAndLevel(id_sub, id_level);
            return Ok(lstQuestion);
        }

        [HttpGet("Get-Question-By-Id_Sub")]
        //[Authorize]
        public async Task<ActionResult<List<QuestionInExam>>> GetAllQuestionById_sub(int id_sub)
        {
            var lstQuestion = await _repoQuestion.GetQuestionBySubjectId(id_sub);
            return Ok(lstQuestion);
        }

        [HttpGet("Get-Count-By-UserId")]
        public async Task<ActionResult<int>> GetCountByUserId(Guid userId)
        {
            var count = await _repoQuestion.GetCountQuestionByUserId(userId);
            return Ok(count);
        }
    }
}
