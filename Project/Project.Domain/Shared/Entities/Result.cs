using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;

namespace Project.Domain.Shared.Entities
{
    public class Result<T>
    {
        public bool Succeeded => !Errors.Any() || Object == null;
        public HashSet<string> Errors { get; set; }
        public T Object { get; set; }

        public Result() { }

        public static Result<T> CreateResult(T objeto, HashSet<string> errors = null)
        {
            return new Result<T>
            {
                Object = objeto,
                Errors = errors ?? new HashSet<string>()
            };
        }


    }
}
