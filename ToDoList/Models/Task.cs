﻿namespace ToDoList.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
