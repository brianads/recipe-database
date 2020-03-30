using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.Generic;
using Recipe_Database.Model;
using System.Collections.ObjectModel;

namespace Recipe_Database.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public partial class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Recipe> _recipeList = new ObservableCollection<Recipe>();

        public IEnumerable<Recipe> RecipeList
        {
            get { return _recipeList; }
        }
        public ObservableCollection<string> _recipeTypeList = new ObservableCollection<string>();

        public IEnumerable<string> RecipeTypeList
        {
            get { return _recipeTypeList; }
        }
        public ObservableCollection<string> _filterTypeList = new ObservableCollection<string>();

        public IEnumerable<string> FilterTypeList
        {
            get { return _filterTypeList; }
        }

        public RelayCommand ViewRecipeButtonCommand { get; set; }
        public RelayCommand EditRecipeButtonCommand { get; set; }
        public RelayCommand DeleteRecipeButtonCommand { get; set; }
        public RelayCommand AddNewRecipeButtonCommand { get; set; }
        public RelayCommand DisplayAllButtonCommand { get; set; }
        public RelayCommand SearchButtonCommand { get; set; }
        public RelayCommand CloseViewRecipeButtonCommand { get; set; }
        public RelayCommand SaveEditRecipeButtonCommand { get; set; }
        public RelayCommand CancelEditRecipeButtonCommand { get; set; }
        public RelayCommand SaveNewRecipeButtonCommand { get; set; }
        public RelayCommand CancelNewRecipeButtonCommand { get; set; }
        public RelayCommand ContinueDeleteButtonCommand { get; set; }
        public RelayCommand CancelDeleteButtonCommand { get; set; }
        public RelayCommand ContinueCloseEditButtonCommand { get; set; }
        public RelayCommand CancelCloseEditButtonCommand { get; set; }
        public RelayCommand ContinueCloseSaveNewButtonCommand { get; set; }
        public RelayCommand CancelCloseSaveNewButtonCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
                ViewRecipeButtonCommand = new RelayCommand(OnViewRecipeCommand, CanExecuteViewRecipeCommand);
                EditRecipeButtonCommand = new RelayCommand(OnEditRecipeCommand, CanExecuteEditRecipeCommand);
                DeleteRecipeButtonCommand = new RelayCommand(OnDeleteRecipeCommand, CanExecuteDeleteRecipeCommand);
                AddNewRecipeButtonCommand = new RelayCommand(OnAddNewRecipeCommand, CanExecuteAddNewRecipeCommand);
                DisplayAllButtonCommand = new RelayCommand(OnDisplayAllCommand, CanExecuteDisplayAllCommand);
                CloseViewRecipeButtonCommand = new RelayCommand(OnCloseViewRecipeCommand, CanExecuteCloseViewRecipeCommand);
                SaveEditRecipeButtonCommand = new RelayCommand(OnSaveEditRecipeCommand, CanExecuteSaveEditRecipeCommand);
                CancelEditRecipeButtonCommand = new RelayCommand(OnCancelEditRecipeCommand, CanExecuteCancelEditRecipeCommand);
                SaveNewRecipeButtonCommand = new RelayCommand(OnSaveNewRecipeCommand, CanExecuteSaveNewRecipeCommand);
                CancelNewRecipeButtonCommand = new RelayCommand(OnCancelNewRecipeCommand, CanExecuteCancelNewRecipeCommand);
                SearchButtonCommand = new RelayCommand(OnSearchCommand, CanExecuteSearchCommand);
                ContinueDeleteButtonCommand = new RelayCommand(OnContinueDeleteCommand, CanExecuteContinueDeleteCommand);
                CancelDeleteButtonCommand = new RelayCommand(OnCancelDeleteCommand, CanExecuteCancelDeleteCommand);
                ContinueCloseEditButtonCommand = new RelayCommand(OnContinueCloseEditCommand, CanExecuteContinueCloseEditCommand);
                CancelCloseEditButtonCommand = new RelayCommand(OnCancelCloseEditCommand, CanExecuteCancelCloseEditCommand);
                ContinueCloseSaveNewButtonCommand = new RelayCommand(OnContinueCloseSaveNewCommand, CanExecuteContinueCloseSaveNewCommand);
                CancelCloseSaveNewButtonCommand = new RelayCommand(OnCancelCloseSaveNewCommand, CanExecuteCancelCloseSaveNewCommand);

                _recipeTypeList.Add("");
                _recipeTypeList.Add("Appetizers");
                _recipeTypeList.Add("Main Dishes");
                _recipeTypeList.Add("Side Dishes");
                _recipeTypeList.Add("Barbecues");
                _recipeTypeList.Add("Cookies");
                _recipeTypeList.Add("Desserts");

                _filterTypeList.Add("All Dishes");
                _filterTypeList.Add("Appetizers");
                _filterTypeList.Add("Main Dishes");
                _filterTypeList.Add("Side Dishes");
                _filterTypeList.Add("Barbecues");
                _filterTypeList.Add("Cookies");
                _filterTypeList.Add("Desserts");

                InitalizeDataBase();

                GetRecipeList(_recipeList);
            }
        }

        private bool CanExecuteCancelCloseSaveNewCommand()
        {
            return true;
        }

        private void OnCancelCloseSaveNewCommand()
        {
            CurrentCardVisualState = "HideCloseSaveNewConfirmation";
        }

        private bool CanExecuteContinueCloseSaveNewCommand()
        {
            return true;
        }

        private void OnContinueCloseSaveNewCommand()
        {
            CurrentCardVisualState = "HideAddNewRecipeForm";
            NewRecipeName = "";
            NewRecipeType = "";
            NewRecipeOrigin = "";
            NewRecipeIngredients = "";
            NewRecipeProcedure = "";
            NewRecipeNotes = "";
        }

        private bool CanExecuteCancelCloseEditCommand()
        {
            return true;
        }

        private void OnCancelCloseEditCommand()
        {
            CurrentCardVisualState = "HideCloseEditConfirmation";
        }

        private bool CanExecuteContinueCloseEditCommand()
        {
            return true;
        }

        private void OnContinueCloseEditCommand()
        {
            CurrentCardVisualState = "HideEditRecipeForm";
            EditRecipeName = "";
            EditRecipeType = "";
            EditRecipeOrigin = "";
            EditRecipeIngredients = "";
            EditRecipeProcedure = "";
            EditRecipeNotes = "";
        }

        private bool CanExecuteCancelDeleteCommand()
        {
            return true;
        }

        private void OnCancelDeleteCommand()
        {
            CurrentCardVisualState = "HideDeleteConfirmation";
        }

        private bool CanExecuteContinueDeleteCommand()
        {
            return true;
        }

        private void OnContinueDeleteCommand()
        {
            DeleteRecipeInDatabase(SelectedRecipe);
            GetRecipeList(_recipeList);
            CurrentCardVisualState = "HideDeleteConfirmation";
        }

        private bool CanExecuteCancelEditRecipeCommand()
        {
            return true;
        }

        private void OnCancelEditRecipeCommand()
        {
            if (!EditRecipeName.Equals(SelectedRecipe.Name) ||
                !EditRecipeType.Equals(SelectedRecipe.Type) ||
                !EditRecipeOrigin.Equals(SelectedRecipe.Origin) ||
                !EditRecipeIngredients.Equals(SelectedRecipe.Ingredients) ||
                !EditRecipeProcedure.Equals(SelectedRecipe.Procedure) ||
                !EditRecipeNotes.Equals(SelectedRecipe.Notes))
            {
                CurrentCardVisualState = "ShowCloseEditConfirmation";
            }
            else
            {
                CurrentCardVisualState = "HideEditRecipeForm";
                EditRecipeName = "";
                EditRecipeType = "";
                EditRecipeOrigin = "";
                EditRecipeIngredients = "";
                EditRecipeProcedure = "";
                EditRecipeNotes = "";
            }
        }

        private bool CanExecuteSaveEditRecipeCommand()
        {
            return true;
        }

        private void OnSaveEditRecipeCommand()
        {
            SelectedRecipe.Name = EditRecipeName;
            SelectedRecipe.Type = EditRecipeType;
            SelectedRecipe.Origin = EditRecipeOrigin;
            SelectedRecipe.Ingredients = EditRecipeIngredients;
            SelectedRecipe.Procedure = EditRecipeProcedure;
            SelectedRecipe.Notes = EditRecipeNotes;
            UpdateRecipeInDatabase(SelectedRecipe);
            GetRecipeList(_recipeList);

            CurrentCardVisualState = "HideEditRecipeForm";
            EditRecipeName = "";
            EditRecipeType = "";
            EditRecipeOrigin = "";
            EditRecipeIngredients = "";
            EditRecipeProcedure = "";
            EditRecipeNotes = "";
        }

        private bool CanExecuteCloseViewRecipeCommand()
        {
            return true;
        }

        private void OnCloseViewRecipeCommand()
        {
            CurrentCardVisualState = "HideViewRecipeForm";
            ViewRecipeName = "";
            ViewRecipeType = "";
            ViewRecipeOrigin = "";
            ViewRecipeIngredients = "";
            ViewRecipeProcedure = "";
            ViewRecipeNotes = "";
        }

        private bool CanExecuteSearchCommand()
        {
            return true;
        }

        private void OnSearchCommand()
        {
            GetSearchResultList(_recipeList, SearchText);
        }

        private bool CanExecuteDisplayAllCommand()
        {
            return true;
        }

        private void OnDisplayAllCommand()
        {
            GetRecipeList(_recipeList);
            SelectedFilterType = "All Dishes";
        }

        private bool CanExecuteEditRecipeCommand()
        {
            return SelectedRecipe != null;
        }

        private void OnEditRecipeCommand()
        {
            EditRecipeName = SelectedRecipe.Name;
            EditRecipeType = SelectedRecipe.Type;
            EditRecipeOrigin = SelectedRecipe.Origin;
            EditRecipeIngredients = SelectedRecipe.Ingredients;
            EditRecipeProcedure = SelectedRecipe.Procedure;
            EditRecipeNotes = SelectedRecipe.Notes;
            CurrentCardVisualState = "ShowEditRecipeForm";
        }

        private bool CanExecuteViewRecipeCommand()
        {
            return SelectedRecipe != null;
        }

        private void OnViewRecipeCommand()
        {
            ViewRecipeName = SelectedRecipe.Name;
            ViewRecipeType = SelectedRecipe.Type;
            ViewRecipeOrigin = SelectedRecipe.Origin;
            ViewRecipeIngredients = SelectedRecipe.Ingredients;
            ViewRecipeProcedure = SelectedRecipe.Procedure;
            ViewRecipeNotes = SelectedRecipe.Notes;
            CurrentCardVisualState = "ShowViewRecipeForm";
        }

        public void RecipeDoubleClicked()
        {
            OnViewRecipeCommand();
        }

        private bool CanExecuteDeleteRecipeCommand()
        {
            return SelectedRecipe != null;
        }

        private void OnDeleteRecipeCommand()
        {
            CurrentCardVisualState = "ShowDeleteConfirmation";
        }

        private bool CanExecuteSaveNewRecipeCommand()
        {
            return true;
        }

        private void OnSaveNewRecipeCommand()
        {
            Recipe newRecipe = new Recipe();
            newRecipe.Name = NewRecipeName;
            newRecipe.Type = NewRecipeType;
            newRecipe.Origin = NewRecipeOrigin;
            newRecipe.Ingredients = NewRecipeIngredients;
            newRecipe.Procedure = NewRecipeProcedure;
            newRecipe.Notes = NewRecipeNotes;
            
            InsertRecipeInDatabase(newRecipe);
            GetRecipeList(_recipeList);
            SelectedFilterType = "All Dishes";
            CurrentCardVisualState = "HideAddNewRecipeForm";
            NewRecipeName = "";
            NewRecipeType = "";
            NewRecipeOrigin = "";
            NewRecipeIngredients = "";
            NewRecipeProcedure = "";
            NewRecipeNotes = "";
        }

        private bool CanExecuteCancelNewRecipeCommand()
        {
            return true;
        }

        private void OnCancelNewRecipeCommand()
        {
            if (!String.IsNullOrEmpty(NewRecipeName) ||
                !String.IsNullOrEmpty(NewRecipeType) ||
                !String.IsNullOrEmpty(NewRecipeOrigin) ||
                !String.IsNullOrEmpty(NewRecipeIngredients) ||
                !String.IsNullOrEmpty(NewRecipeProcedure) ||
                !String.IsNullOrEmpty(NewRecipeNotes))
            {
                CurrentCardVisualState = "ShowCloseSaveNewConfirmation";
            }
            else
            {
                CurrentCardVisualState = "HideAddNewRecipeForm";
                NewRecipeName = "";
                NewRecipeType = "";
                NewRecipeOrigin = "";
                NewRecipeIngredients = "";
                NewRecipeProcedure = "";
                NewRecipeNotes = "";
            }
        }

        private bool CanExecuteAddNewRecipeCommand()
        {
            return true;
        }

        private void OnAddNewRecipeCommand()
        {
            CurrentCardVisualState = "ShowAddNewRecipeForm";
        }

        public void TypeSelectionChanged()
        {
            if (SelectedFilterType.Equals("All Dishes"))
            {
                GetRecipeList(_recipeList);
            }
            else
            {
                GetRecipeList(_recipeList, SelectedFilterType);
            }
        }

        public void SetButtonStates()
        {
            ViewRecipeButtonCommand.RaiseCanExecuteChanged();
            EditRecipeButtonCommand.RaiseCanExecuteChanged();
            DeleteRecipeButtonCommand.RaiseCanExecuteChanged();
            
        }

        #region SelectedRecipe      
        private Recipe _selectedRecipe = null;
        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                if (_selectedRecipe == value) return;

                _selectedRecipe = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SearchText      
        private string _searchText = "";
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText == value) return;

                _searchText = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ViewRecipeName      
        private string _viewRecipeName = "";
        public string ViewRecipeName
        {
            get { return _viewRecipeName; }
            set
            {
                if (_viewRecipeName == value) return;

                _viewRecipeName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ViewRecipeType      
        private string _viewRecipeType = "";
        public string ViewRecipeType
        {
            get { return _viewRecipeType; }
            set
            {
                if (_viewRecipeType == value) return;

                _viewRecipeType = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ViewRecipeOrigin      
        private string _viewRecipeOrigin = "";
        public string ViewRecipeOrigin
        {
            get { return _viewRecipeOrigin; }
            set
            {
                if (_viewRecipeOrigin == value) return;

                _viewRecipeOrigin = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ViewRecipeIngredients      
        private string _viewRecipeIngredients = "";
        public string ViewRecipeIngredients
        {
            get { return _viewRecipeIngredients; }
            set
            {
                if (_viewRecipeIngredients == value) return;

                _viewRecipeIngredients = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ViewRecipeProcedure      
        private string _viewRecipeProcedure = "";
        public string ViewRecipeProcedure
        {
            get { return _viewRecipeProcedure; }
            set
            {
                if (_viewRecipeProcedure == value) return;

                _viewRecipeProcedure = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ViewRecipeNotes      
        private string _viewRecipeNotes = "";
        public string ViewRecipeNotes
        {
            get { return _viewRecipeNotes; }
            set
            {
                if (_viewRecipeNotes == value) return;

                _viewRecipeNotes = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EditRecipeName      
        private string _editRecipeName = "";
        public string EditRecipeName
        {
            get { return _editRecipeName; }
            set
            {
                if (_editRecipeName == value) return;

                _editRecipeName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EditRecipeType      
        private string _editRecipeType = "";
        public string EditRecipeType
        {
            get { return _editRecipeType; }
            set
            {
                if (_editRecipeType == value) return;

                _editRecipeType = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EditRecipeOrigin      
        private string _editRecipeOrigin = "";
        public string EditRecipeOrigin
        {
            get { return _editRecipeOrigin; }
            set
            {
                if (_editRecipeOrigin == value) return;

                _editRecipeOrigin = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EditRecipeIngredients      
        private string _editRecipeIngredients = "";
        public string EditRecipeIngredients
        {
            get { return _editRecipeIngredients; }
            set
            {
                if (_editRecipeIngredients == value) return;

                _editRecipeIngredients = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EditRecipeProcedure      
        private string _editRecipeProcedure = "";
        public string EditRecipeProcedure
        {
            get { return _editRecipeProcedure; }
            set
            {
                if (_editRecipeProcedure == value) return;

                _editRecipeProcedure = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EditRecipeNotes      
        private string _editRecipeNotes = "";
        public string EditRecipeNotes
        {
            get { return _editRecipeNotes; }
            set
            {
                if (_editRecipeNotes == value) return;

                _editRecipeNotes = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region NewRecipeName      
        private string _newRecipeName = "";
        public string NewRecipeName
        {
            get { return _newRecipeName; }
            set
            {
                if (_newRecipeName == value) return;

                _newRecipeName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region NewRecipeType      
        private string _newRecipeType = "";
        public string NewRecipeType
        {
            get { return _newRecipeType; }
            set
            {
                if (_newRecipeType == value) return;

                _newRecipeType = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region NewRecipeOrigin      
        private string _newRecipeOrigin = "";
        public string NewRecipeOrigin
        {
            get { return _newRecipeOrigin; }
            set
            {
                if (_newRecipeOrigin == value) return;

                _newRecipeOrigin = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region NewRecipeIngredients      
        private string _newRecipeIngredients = "";
        public string NewRecipeIngredients
        {
            get { return _newRecipeIngredients; }
            set
            {
                if (_newRecipeIngredients == value) return;

                _newRecipeIngredients = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region NewRecipeProcedure      
        private string _newRecipeProcedure = "";
        public string NewRecipeProcedure
        {
            get { return _newRecipeProcedure; }
            set
            {
                if (_newRecipeProcedure == value) return;

                _newRecipeProcedure = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region NewRecipeNotes      
        private string _newRecipeNotes = "";
        public string NewRecipeNotes
        {
            get { return _newRecipeNotes; }
            set
            {
                if (_newRecipeNotes == value) return;

                _newRecipeNotes = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SelectedFilterType      
        private string _selectedFilterType = "";
        public string SelectedFilterType
        {
            get { return _selectedFilterType; }
            set
            {
                if (_selectedFilterType == value) return;

                _selectedFilterType = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CurrentCardVisualState      
        private string _currentCardVisualState = "";
        public string CurrentCardVisualState
        {
            get { return _currentCardVisualState; }
            set
            {
                if (_currentCardVisualState == value) return;

                _currentCardVisualState = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}