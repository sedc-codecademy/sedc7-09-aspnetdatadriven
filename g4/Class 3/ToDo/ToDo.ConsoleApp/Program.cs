using System;
using System.Collections.Generic;

namespace ToDo.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //InsertTaskTest();

            //UpdateTaskTest();
            DeleteTaskTest();
            //GetCountTest();
            //Console.ReadLine();

            //GetAllTasksTest();

            //Console.WriteLine("Enter task id:");
            //int taskId = int.Parse(Console.ReadLine());

            //var taskRepository = new DapperTaskRepository();
            //var task = taskRepository.GetByIdAsync(taskId).GetAwaiter().GetResult();

            //PrintTask(task);
            //string taskId = Console.ReadLine();
            //var task = taskRepository.GetById(taskId);

            //Console.WriteLine("Enter task title:");
            //string taskTitle = Console.ReadLine();
            //var task = taskRepository.GetByTitle(taskTitle);

            //Console.WriteLine($"Task Id:{task.Id} Title:{task.Title} Description:{task.Description} Priority:{task.Priority} Status:{task.Status} Type:{task.Type}");
            Console.ReadLine();
        }

        private static void PrintTask(DataLayer.Entities.Task task)
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"Task Id:{task.Id} Title:{task.Title} Description:{task.Description} Priority:{task.Priority} Status:{task.Status} Type:{task.Type}");
            foreach (var subtask in task.SubTasks)
            {
                Console.WriteLine($"Subtask Id: {subtask.Id}, Title: {subtask.Title}, Description: {subtask.Description}, Status: {subtask.Status},  ");
            }
            Console.WriteLine("---------------------------------------------------");
        }

        private static void GetAllTasksTest()
        {
            var taskRepository = new DapperTaskRepository();
            IEnumerable<DataLayer.Entities.Task> tasks = taskRepository.GetAllAsync()
                                                                        .GetAwaiter()
                                                                        .GetResult();
            foreach (var task in tasks)
            {
                PrintTask(task);
            }
            Console.WriteLine("Finished with tasks printing");
        }

        private static void GetCountTest()
        {
            var taskRepository = new AdoNetTasksDbRepository();

            var tasksCount = taskRepository.GetCount();
            Console.WriteLine($"There are {tasksCount} tasks in the database");

            Console.ReadLine();
        }

        private static void DeleteTaskTest()
        {
            var taskRepository = new DapperTaskRepository();

            Console.WriteLine("Enter existing task id");
            var id = int.Parse(Console.ReadLine());

            var task = taskRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (task == null)
            {
                Console.WriteLine($"Task with id: {id} does not exist");
                return;
            }


            taskRepository.RemoveAsync(task).GetAwaiter().GetResult();
            Console.WriteLine("Successful delete");

            Console.ReadLine();
        }

        private static void UpdateTaskTest()
        {
            var taskRepository = new AdoNetTasksDbRepository();
            
            Console.WriteLine("Enter existing task id");
            var id = int.Parse(Console.ReadLine());

            var task = taskRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (task == null)
            {
                Console.WriteLine($"Task with id: {id} does not exist");
                return;
            }

            Console.WriteLine("Enter task title");
            task.Title = Console.ReadLine();

            Console.WriteLine("Enter task description");
            task.Description = Console.ReadLine();

            Console.WriteLine("Enter task status");
            task.Status = (DataLayer.Enums.Status)int.Parse(Console.ReadLine());

            Console.WriteLine("Enter task type");
            task.Type = (DataLayer.Enums.TaskType)int.Parse(Console.ReadLine());

            Console.WriteLine("Enter task priority");
            task.Priority = (DataLayer.Enums.Priority)int.Parse(Console.ReadLine());

            taskRepository.UpdateAsync(task).GetAwaiter().GetResult();
            Console.WriteLine("Successful update");

            Console.ReadLine();
        }

        private static void InsertTaskTest()
        {
            var taskRepository = new DapperTaskRepository();
            var task = new DataLayer.Entities.Task();

            Console.WriteLine("Enter task title");
            task.Title = Console.ReadLine();

            Console.WriteLine("Enter task description");
            task.Description = Console.ReadLine();

            Console.WriteLine("Enter task status");
            task.Status = (DataLayer.Enums.Status)int.Parse(Console.ReadLine());

            Console.WriteLine("Enter task type");
            task.Type = (DataLayer.Enums.TaskType)int.Parse(Console.ReadLine());

            Console.WriteLine("Enter task priority");
            task.Priority = (DataLayer.Enums.Priority)int.Parse(Console.ReadLine());

            taskRepository.AddAsync(task).GetAwaiter().GetResult();
            Console.WriteLine($"Id of the new inserted task: {task.Id}");

            Console.ReadLine();
        }
    }
}
