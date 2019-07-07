namespace ToDo.DataLayer.Entities
{
    public class SubTask : BaseTask
    {
        public Task ParentTask { get; set; }

        public int ParentTaskId { get; set; }
    }
}
