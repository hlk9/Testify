using Microsoft.AspNetCore.Components.Forms;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;
using Testify.DAL.ViewModels;

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

        public async Task<List<Question>> GetAllQuestions(string? textSearch, bool isActive)
        {
            var allQuestion = await _httpClient.GetAsync($"Question/Get-All-Questions?keyWord={textSearch}&isActive={isActive}");
            var response = await allQuestion.Content.ReadFromJsonAsync<List<Question>>();

            return response;
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Question>($"Question/Get-Question-By-Id?id={id}");
        }

        public async Task<HttpResponseMessage> ExportExcelQuestion()
        {
            return await _httpClient.GetAsync("Question/Export-Excel-Template-Question");
        }

        public async Task<HttpResponseMessage> ExportQuestionBySubjectId(int subjectId, bool isAnswer)
        {
            return await _httpClient.GetAsync($"Question/Export-Question-By-SubjectId?subjectId={subjectId}&isAnswer={isAnswer}");
        }

        public async Task<int> ImportExcelQuestion(IBrowserFile file, int subjectId)
        {
            using var content = new MultipartFormDataContent();

            using var stream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(stream);
            stream.Position = 0;

            content.Add(new StreamContent(stream), "file", file.Name);
            content.Add(new StringContent(subjectId.ToString()), "subjectId");

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

        public async Task<Question> DeleteQuestion(int id)
        {
            var deleteQuestion = await _httpClient.DeleteAsync($"Question/Delete-Question?id={id}");
            var reponse = await deleteQuestion.Content.ReadFromJsonAsync<Question>();
            return reponse;
        }

        public async Task<AnswerAndQuestion> GetTrueAnswerOfQuesiton(int questionId, int examdetailId)
        {
            var obj = await _httpClient.GetAsync($"Question/Get-AnswerIsTrue-Point-Question?questionId={questionId}&examDetailId={examdetailId}");
            var response = await obj.Content.ReadFromJsonAsync<AnswerAndQuestion>();

            return response;
        }
    }
}
