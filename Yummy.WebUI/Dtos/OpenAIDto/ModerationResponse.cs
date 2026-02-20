namespace Yummy.WebUI.Dtos.OpenAIDto
{
    public class ModerationResponse
    {
        public List<ModerationResult> Results { get; set; }
    }

    public class ModerationResult
    {
        public bool Flagged { get; set; }
        public Dictionary<string, bool> Categories { get; set; }
        public Dictionary<string, double> Category_Scores { get; set; }
    }

}
