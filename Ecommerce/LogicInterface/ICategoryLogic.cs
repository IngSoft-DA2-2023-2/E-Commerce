namespace LogicInterface
{
    public interface ICategoryLogic
    {
        public bool CheckForCategory(string category);
        public IEnumerable<string> GetCategories();
    }
}