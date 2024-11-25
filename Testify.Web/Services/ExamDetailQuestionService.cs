using Testify.DAL.Models;
using Testify.DAL.ViewModels;

namespace Testify.Web.Services
{
    public class ExamDetailQuestionService
    {
        private readonly HttpClient _httpClient;
        public ExamDetailQuestionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ExamDetailQuestion>> GetAllByExamDetailId(int examDetailId)
        {
            return await _httpClient.GetFromJsonAsync<List<ExamDetailQuestion>>($"ExamDetailQuestion/Get-ExamDetailQuestion-By-ExamDetailID?examDetailID={examDetailId}");
        }

        public async Task<ExamDetailQuestion> CreateExamDetailQuestion(ExamDetailQuestion examDetailQuestion)
        {
            var objNew = await _httpClient.PostAsJsonAsync("ExamDetailQuestion/Create", examDetailQuestion);
            var reponse = await objNew.Content.ReadFromJsonAsync<ExamDetailQuestion>();
            return reponse;
        }

        public async Task<bool> DeleteExamDetailQuestionsByExamDetailId(int idExamDetail)
        {
            var status = await _httpClient.DeleteAsync($"ExamDetailQuestion/Delete?idExamDetail={idExamDetail}");
            if (status.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<QuestionInExam>> GetAllQuestionByExamDetailIDAsync(int examDetailID)
        {
            var lst = await _httpClient.GetAsync($"ExamDetailQuestion/Get-Question-By-ExamDetailID?examdetailID={examDetailID}");
            var response = await lst.Content.ReadFromJsonAsync<List<QuestionInExam>>();
            return response;
        }

        public async Task<List<QuestionInExam>> GetAllQuestionByExamDetailIDAsync_NOT(int examDetailID, int SubjectId)
        {
            var lst = await _httpClient.GetAsync($"ExamDetailQuestion/Get-Question-By-ExamDetailID-Not?examdetailID={examDetailID}&SubjectId={SubjectId}");
            var response = await lst.Content.ReadFromJsonAsync<List<QuestionInExam>>();
            return response;
        }

        public async Task<List<QuestionInExam>> GetAllQuestionByExamDetailIDAsync_NOTAndLevel(int examDetailID, int levelID, int SubjectId)
        {
            var lst = await _httpClient.GetAsync($"ExamDetailQuestion/Get-Question-By-ExamDetailID-NotAndLevel?examdetailID={examDetailID}&levelID={levelID}&SubjectId={SubjectId}");
            var response = await lst.Content.ReadFromJsonAsync<List<QuestionInExam>>();
            return response;
        }


        public async Task<bool> AddListQuestionToExam(List<QuestionInExam> data, int idExamDetail)
        {
            var a = await _httpClient.PostAsJsonAsync("ExamDetailQuestion/Add-ListQuestionToExam?idExamDetail=" + idExamDetail, data);
            if (a.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveFromListQuestionToExam(List<QuestionInExam> data, int idExamDetail)
        {
            var a = await _httpClient.PostAsJsonAsync("ExamDetailQuestion/Remove-ListQuestionToExam?idExamDetail=" + idExamDetail, data);
            if (a.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


    }
}
