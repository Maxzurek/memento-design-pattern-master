using qa_eval_finale_memento.caretakers;
using qa_eval_finale_memento.commands;
using qa_eval_finale_memento.mementos;
using qa_eval_finale_memento.states;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace qa_eval_finale_memento.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IOriginator
    {
        #region Constructor

        public MainViewModel()
        {
            caretaker = new Caretaker(this);

            undoCommand = new UndoCommand(caretaker);
            redoCommand = new RedoCommand(caretaker);
            eraseCommand = new EraseCommand(caretaker);
            pasteCommand = new PasteCommand(caretaker);

            caretaker.PropertyChanged += HandleCaretakerPropertyChanged;

            UndosHistory = caretaker.GetUndosHistory();
        }

        #endregion

        #region Properties

        private readonly List<string> ALPHANUMERIC_CHARS = new List<string>("a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,1,2,3,4,5,6,7,8,9,0".Split(','));

        public ICommand undoCommand { get; }
        public ICommand redoCommand { get; }
        public ICommand eraseCommand { get; }
        public ICommand pasteCommand { get; }
        private readonly Caretaker caretaker;

        public bool StateRestored { get; private set; } = false;

        private string text = "";
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        private int caretPosition;
        public int CaretPosition
        {
            get
            {
                return caretPosition;
            }
            set
            {
                caretPosition = value;
                OnPropertyChanged(nameof(CaretPosition));
            }
        }

        private ObservableCollection<string> undosHistory = new();
        public ObservableCollection<string> UndosHistory
        {
            get
            {
                return undosHistory;
            }
            set
            {
                undosHistory = value;
                OnPropertyChanged(nameof(UndosHistory));
            }
        }

        private ObservableCollection<string> redosHistory = new();
        public ObservableCollection<string> RedosHistory
        {
            get
            {
                return redosHistory;
            }
            set
            {
                redosHistory = value;
                OnPropertyChanged(nameof(RedosHistory));
            }
        }

        #endregion

        #region Interface Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        public IMemento SaveState()
        {
            IState state = new TextBoxState(Text.TrimEnd(), CaretPosition);

            return new TextBoxMemento(state);
        }

        public void RestoreState(IMemento memento)
        {
            if (memento is not TextBoxMemento)
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            StateRestored = true;

            IState state = memento.GetState();

            if (state is TextBoxState textBoxState)
            {
                Text = textBoxState.Text;
                CaretPosition = textBoxState.CaretPosition;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Backup this current state whenever the Enter key is pressed inside the Textbox control of the MainView
        /// </summary>
        public void HandleMainViewEnterKeyDown()
        {
            caretaker.Backup();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get the undos or redos history whenever the undos or redos property of the Caretaker is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCaretakerPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e != null)
            {
                string? propertyName = e.PropertyName;

                switch (propertyName)
                {
                    case "undos":
                        UndosHistory = caretaker.GetUndosHistory();
                        break;
                    case "redos":
                        RedosHistory = caretaker.GetRedosHistory();
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(Text))
            {
                HandleTextBoxTextChanged();
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void HandleTextBoxTextChanged()
        {
            if (StateRestored)
            {
                StateRestored = !StateRestored;
            }
            else
            {
                if (Text.Length == 0)
                {
                    return;
                }
                else if (
                    Text.Length > 1 && // We have at least 1 char
                    CaretPosition == Text.Length - 1 && // 
                    !ALPHANUMERIC_CHARS.Contains(Text[^1].ToString().ToLower())
                    && Text[^1] != ' ')
                // Whenever the user enters a non alphanumeric character a the end of the textbox,
                // since this method is called before the CaretPosition is set,
                // we want to move the textbox's CaretPosition to the end of the of the textbox before doing a backup of the state
                {
                    CaretPosition++;
                    caretaker.Backup();
                }
                else if (Text.Length > 1) // We have at least 1 character
                {
                    if (Text[^1] == ' ' && Text[^2] != ' ')
                    // Whenever last character typed is a space and the character before is not a space, we have a "word".
                        // Backup the current state
                    {
                        caretaker.Backup();
                    }
                }
            }
        }

        #endregion
    }
}
