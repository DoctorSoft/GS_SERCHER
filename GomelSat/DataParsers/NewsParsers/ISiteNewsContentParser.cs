namespace DataParsers.NewsParsers
{
    public interface ISiteNewsContentParser<TNewsContentModel>
    {
        TNewsContentModel GetContent(TNewsContentModel pageTextModel);
    }
}
