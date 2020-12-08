using AdventOfCode.Day8.Entities;
using AdventOfCode.Day8.Enums;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day8
{
    public class Day8Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public HandheldProgram _program;

        public Day8Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitProgramData();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            RunProgramUntiLoopDetected();
            SetAnswer(_program.Accumulator);

            StartTime();
            FixProgram();
            SetAnswer(_program.Accumulator);
        }

        public void RunProgramUntiLoopDetected()
        {
            while (ValidateCurrentIndex(_program.CurrentIndex) && ValidateTimesCalled(_program.CurrentIndex))
            {
                var instruction = _program.Instructions[_program.CurrentIndex];
                ExecuteInstruction(instruction);
            }
        }

        public bool FixProgram()
        {
            var instructionsToChange = _program.Instructions.Where(i => i.Opperation == Opperation.nop || i.Opperation == Opperation.jmp);

            foreach(var instruction in instructionsToChange)
            {
                ChangeInstruction(instruction);
                ResetProgram();
                RunProgramUntiLoopDetected();
                if (_program.CurrentIndex >= _program.Instructions.Count)
                {
                    return true;
                }
                else
                {
                    ChangeInstruction(instruction);
                }
            }

            return false;
        }

        private void ResetProgram()
        {
            _program.Accumulator = 0;
            _program.CurrentIndex = 0;
            _program.Instructions = _program.Instructions.Select(i => { i.TimesCalled = 0; return i; }).ToList();
        }

        private void ChangeInstruction(Instruction instruction)
        {
            switch(instruction.Opperation)
            {
                case Opperation.jmp:
                {
                    instruction.Opperation = Opperation.nop;
                    instruction.TimesCalled = 0;
                    break;
                }
                case Opperation.nop:
                {
                    if (instruction.Offset != 0)
                    {
                        instruction.Opperation = Opperation.jmp;
                        instruction.TimesCalled = 0;
                    }
                    break;
                }
            }
        }

        private void ExecuteInstruction(Instruction instruction)
        {
            switch(instruction.Opperation)
            {
                case Opperation.acc:
                {
                    _program.Accumulator = _program.Accumulator + instruction.Offset;
                    _program.CurrentIndex++;
                    instruction.TimesCalled++;
                    break;
                }
                case Opperation.jmp:
                {
                    _program.CurrentIndex = _program.CurrentIndex + instruction.Offset;
                    instruction.TimesCalled++;
                    break;
                }
                case Opperation.nop:
                {
                    _program.CurrentIndex++;
                    instruction.TimesCalled++;
                    break;
                }
            }
        }

        private bool ValidateCurrentIndex(int index)
        {
            if (index >= 0 && index < _program.Instructions.Count)
            {
                return true;
            }
            return false;
        }

        private bool ValidateTimesCalled(int index)
        {
            if (_program.Instructions[index].TimesCalled > 0)
            {
                return false;
            }
            return true;
        }

        private void InitProgramData()
        {
            var rawInstructions = _fileReader.ReadFileToStringArray("Day8/data.json");
            MapRawDataToProgram(rawInstructions);
        }

        private void MapRawDataToProgram(List<string> rawInstructions)
        {
            _program = new HandheldProgram() 
            {
               Accumulator = 0,
               CurrentIndex = 0,
               Instructions = new List<Instruction>()
            };

            foreach(var instruction in rawInstructions)
            {
                var newInstruction = new Instruction()
                {
                    Opperation = GetOpperationFromRawData(instruction.Split()[0]),
                    Offset = GetOffsetFromRawData(instruction.Split()[1]),
                    TimesCalled = 0
                };

                _program.Instructions.Add(newInstruction);
            }
        }

        private Opperation GetOpperationFromRawData(string rawData)
        {
            return rawData switch
            {
                "acc" => Opperation.acc,
                "jmp" => Opperation.jmp,
                "nop" => Opperation.nop
            };
        }

        private int GetOffsetFromRawData(string rawData)
        {
            return rawData[0] switch
            {
                '-' => (Int32.Parse(rawData.Replace("-", "")) * -1),
                '+' => Int32.Parse(rawData.Replace("+", ""))
            };
        }
    }
}
