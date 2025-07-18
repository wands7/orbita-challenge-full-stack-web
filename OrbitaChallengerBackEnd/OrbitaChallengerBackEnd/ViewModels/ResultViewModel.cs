﻿namespace OrbitaChallengerBackEnd.ViewModels
{
    public class ResultViewModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResultViewModel(bool success, string message, T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
