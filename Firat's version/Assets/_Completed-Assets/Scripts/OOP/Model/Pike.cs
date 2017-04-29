
namespace Model
{
    public class Pike : Platform
    {

        public Pike(int length, int width, int x, int y) : base(length, width, x, y)
        {
            LoadPrefab("Pike");
        }
    }
}