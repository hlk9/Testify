using Microsoft.AspNetCore.Components.Forms;
using SendGrid.Helpers.Mail;
using Testify.DAL.Models;
using Testify.DAL.ViewModels;
using Testify.Web.Components.Pages.Examiner;

namespace Testify.Web.Services
{
    public class QuestionService
    {
        private readonly HttpClient _httpClient;

        public QuestionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(60);
        }

        public async Task<List<Question>> GetAllQuestions(string? textSearch, Guid? userId)
        {
            var allQuestion = await _httpClient.GetAsync($"Question/Get-All-Questions?keyWord={textSearch}&userId={userId}");
            var response = await allQuestion.Content.ReadFromJsonAsync<List<Question>>();

            return response;
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Question>($"Question/Get-Question-By-Id?id={id}");
        }

        public async Task<List<QuestionInExam>> GetAllQuestioneByIdSub(int id_sub)
        {


            var response = await _httpClient.GetAsync($"Question/Get-Question-By-Id_Sub?id_sub={id_sub}");

            if (response.IsSuccessStatusCode)
            {

                var questions = await response.Content.ReadFromJsonAsync<List<QuestionInExam>>();
                return questions ?? new List<QuestionInExam>();
            }
            else
            {

                return new List<QuestionInExam>();
            }
        }

        public async Task<List<QuestionInExam>> GetAllQuestioneByIdSub_And_Level(int id_sub, int id_level)
        {


            var response = await _httpClient.GetAsync($"Question/Get-Question-By-Id_Sub-And-Level?id_sub={id_sub}&id_level={id_level}");

            if (response.IsSuccessStatusCode)
            {
                // Chuyển đổi dữ liệu JSON từ phản hồi thành danh sách câu hỏi
                var questions = await response.Content.ReadFromJsonAsync<List<QuestionInExam>>();
                return questions ?? new List<QuestionInExam>();
            }
            else
            {
                // Nếu không thành công, trả về danh sách rỗng hoặc ném lỗi
                return new List<QuestionInExam>();
            }
        }

        public async Task<HttpResponseMessage> ExportExcelQuestion()
        {
            return await _httpClient.GetAsync("Question/Export-Excel-Template-Question");
        }

        public async Task<HttpResponseMessage> ExportQuestionBySubjectId(int subjectId, bool isAnswer)
        {
            return await _httpClient.GetAsync($"Question/Export-Question-By-SubjectId?subjectId={subjectId}&isAnswer={isAnswer}");
        }

        public async Task<bool> Checkvalidate(string content, int questionTypeId, int subjectId, int? questionId)
        {
            string encodedContent = Uri.EscapeDataString(content);
            var hasQuestion = await _httpClient.GetAsync($"Question/Check-Validate?content={encodedContent}&questionTypeId={questionTypeId}&subjectId={subjectId}&questionId={questionId}");
            var response = await hasQuestion.Content.ReadFromJsonAsync<bool>();
            return response;
        }

        public async Task<bool> CheckUpdate(int questionId)
        {
            var hasQuestion = await _httpClient.GetAsync($"Question/Check-Update?questionId={questionId}");
            var response = await hasQuestion.Content.ReadFromJsonAsync<bool>();
            return response;
        }

        public async Task<int> ImportExcelQuestion(IBrowserFile file, int subjectId, Guid? userId)
        {
            using var content = new MultipartFormDataContent();

            using var stream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(stream);
            stream.Position = 0;

            content.Add(new StreamContent(stream), "file", file.Name);
            content.Add(new StringContent(subjectId.ToString()), "subjectId");
            content.Add(new StringContent(userId.ToString()), "userId");

            var allQuestion = await _httpClient.PostAsync("Question/Import-Excel-Question", content);
            var response = await allQuestion.Content.ReadFromJsonAsync<int>();

            return response;
        }

        public async Task<Question> CreateQuestion(Question question)
        {
            var newQuestion = await _httpClient.PostAsJsonAsync("Question/Create-Question", question);
            var reponse = await newQuestion.Content.ReadFromJsonAsync<Question>();
            return reponse;
        }

        public async Task<Question> UpdateQuestion(Question question)
        {
            var updateQuestion = await _httpClient.PutAsJsonAsync("Question/Update-Question", question);
            var reponse = await updateQuestion.Content.ReadFromJsonAsync<Question>();
            return reponse;
        }

        public async Task<Question> UpdateUpload(Question question)
        {
            var updateQuestion = await _httpClient.PutAsJsonAsync("Question/Update-UploadFile-Question", question);
            var reponse = await updateQuestion.Content.ReadFromJsonAsync<Question>();
            return reponse;
        }

        public async Task<Question> UpdateStatus(int id, byte status)
        {
            var updateStatus = await _httpClient.PutAsJsonAsync($"Question/Update-Status?questionId={id}&status={status}", status);
            var reponse = await updateStatus.Content.ReadFromJsonAsync<Question>();
            return reponse;
        }

        public async Task<ErrorResponse> DeleteQuestion(int id)
        {
            var deleteQuestion = await _httpClient.DeleteAsync($"Question/Delete-Question?id={id}");

            if (deleteQuestion.IsSuccessStatusCode)
            {
                return new ErrorResponse { Success = true };
            }

            var error = await deleteQuestion.Content.ReadFromJsonAsync<ErrorResponse>();
            return new ErrorResponse
            {
                Success = false,
                ErrorCode = error?.ErrorCode ?? "UNKNOWN_ERROR",
                Message = error?.Message ?? "UNKNOWN_ERROR"
            };
        }

        public async Task<AnswerAndQuestion> GetTrueAnswerOfQuesiton(int questionId, int examdetailId)
        {
            var obj = await _httpClient.GetAsync($"Question/Get-AnswerIsTrue-Point-Question?questionId={questionId}&examDetailId={examdetailId}");
            var response = await obj.Content.ReadFromJsonAsync<AnswerAndQuestion>();

            return response;
        }

        public async Task<int> GetCountByUserId(Guid userId)
        {
            var count = await _httpClient.GetAsync($"Question/Get-Count-By-UserId?userId={userId}");
            var response = await count.Content.ReadFromJsonAsync<int>();
            return response;
        }
    }
}
