﻿using qa_eval_finale_memento.mementos;
using qa_eval_finale_memento.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace qa_eval_finale_memento.caretakers
{
    public class Caretaker : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IOriginator originator;
        private readonly Stack<IMemento> undos = new();
        private readonly Stack<IMemento> redos = new();

        public Caretaker(IOriginator originator)
        {
            this.originator = originator;
            Backup();
        }

        public void Backup()
        {
            if (undos.Count == 0)
            {
                IMemento? memento;

                if (redos.TryPeek(out memento))
                {
                    undos.Push(memento);
                }
            }
            redos.Clear();
            undos.Push(originator.SaveState());
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(undos)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(redos)));
        }

        public bool Undo()
        {
            if (undos.Count == 0)
            {
                return false;
            }

            IMemento memento = undos.Pop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(undos)));

            try
            {
                redos.Push(memento);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(redos)));
                originator.RestoreState(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }

            return true;
        }

        public bool Redo()
        {
            if (redos.Count == 0)
            {
                return false;
            }

            IMemento memento = redos.Pop();
            undos.Push(memento);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(undos)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(redos)));

            try
            {
                originator.RestoreState(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }

            return true;
        }

        public ObservableCollection<string> GetUndosHistory()
        {
            ObservableCollection<string> undosHistory = new();

            foreach (var memento in undos)
            {
                undosHistory.Add(memento.GetName());
            }

            return undosHistory;
        }

        public ObservableCollection<string> GetRedosHistory()
        {
            ObservableCollection<string> redosHistory = new();

            foreach (var memento in redos)
            {
                redosHistory.Add(memento.GetName());
            }

            return redosHistory;
        }
    }
}
