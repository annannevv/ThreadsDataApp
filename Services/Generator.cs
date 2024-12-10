using ConcurrentDataApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentDataApp.Services
{
    internal class Generator : IGenerator
    {
        private static readonly string _letters = "abcdefghijklmnopqrstuvwxyz";
        private static Random _random;

        public Generator(Random random) 
        { 
            _random = random;
        }
        
        public async Task<string> GenerateNameAsync()
        {
            return await Task.Run(() =>
            {
                var nameBuilder = new StringBuilder();
                for (int i = 0; i < 5; i++)
                {
                    char letter = _letters[_random.Next(_letters.Length)];
                    nameBuilder.Append(letter);
                }
                return nameBuilder.ToString();
            });
        }

        public async Task<string> GenerateNumberAsync()
        {
            return await Task.Run(() =>
            {
                var phoneBuilder = new StringBuilder("+380");
                for (int i = 0; i < 9; i++) 
                {
                    phoneBuilder.Append(_random.Next(0, 10));
                }
                return phoneBuilder.ToString();
            });
        }
    }
}
