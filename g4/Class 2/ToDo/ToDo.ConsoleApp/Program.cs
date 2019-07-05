﻿using System;
using System.Collections.Generic;

namespace ToDo.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskRepository = new AdoNetTasksDbRepository();
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

            //IEnumerable<DataLayer.Entities.Task> tasks = taskRepository.GetAllAsync()
            //                                                            .GetAwaiter()
            //                                                            .GetResult();
            //foreach (var task in tasks)
            //{
            //    Console.WriteLine("---------------------------------------------------");
            //    Console.WriteLine($"Task Id:{task.Id} Title:{task.Title} Description:{task.Description} Priority:{task.Priority} Status:{task.Status} Type:{task.Type}");
            //    foreach (var subtask in task.SubTasks)
            //    {
            //        Console.WriteLine($"Subtask Id: {subtask.Id}, Title: {subtask.Title}, Description: {subtask.Description}, Status: {subtask.Status},  ");
            //    }
            //    Console.WriteLine("---------------------------------------------------");
            //}
            //Console.WriteLine("Finished with tasks printing");
            //Console.WriteLine("Enter task id:");
            //int taskId = int.Parse(Console.ReadLine());
            //var task = taskRepository.GetByIdAsync(taskId).GetAwaiter().GetResult();
            //string taskId = Console.ReadLine();
            //var task = taskRepository.GetById(taskId);

            //Console.WriteLine("Enter task title:");
            //string taskTitle = Console.ReadLine();
            //var task = taskRepository.GetByTitle(taskTitle);

            //Console.WriteLine($"Task Id:{task.Id} Title:{task.Title} Description:{task.Description} Priority:{task.Priority} Status:{task.Status} Type:{task.Type}");
            //Console.ReadLine();
        }
    }
}
