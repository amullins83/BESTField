namespace BESTTieBreaker.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Windows.Threading;

    using BEST2014;
    using Models;

    public class TieBreakerViewModel : ViewModel
    {
        /// <summary>
        /// The currently active field
        /// </summary>
        private IField field;

        /// <summary>
        /// The currently active field model
        /// </summary>
        private FieldModel fieldModel;

        /// <summary>
        /// An observable collection of quadrant results
        /// </summary>
        private ObservableCollection<QuadrantResultModel> results =
            new ObservableCollection<QuadrantResultModel>();

        /// <summary>
        /// Interval timer for polling the field for results
        /// </summary>
        private DispatcherTimer pollTimer;

        /// <summary>
        /// Error message to display
        /// </summary>
        private string errorMessage = "No field detected";

        /// <summary>
        /// Gets or sets the currently active field model
        /// </summary>
        public FieldModel FieldModel
        {
            get
            {
                return this.fieldModel;
            }

            set
            {
                if (pollTimer != null)
                {
                    pollTimer.Stop();
                }

                SetProperty(ref this.fieldModel, value);

                RaisePropertyChanged("Id");

                if (this.fieldModel != null)
                {
                    this.field = this.fieldModel.Field;

                    this.pollTimer =
                        new DispatcherTimer(
                            TimeSpan.FromMilliseconds(250),
                            DispatcherPriority.Background,
                            this.GetResults,
                            Dispatcher.CurrentDispatcher);
                }
            }
        }

        /// <summary>
        /// Gets the ID of the active field
        /// </summary>
        public int Id
        {
            get
            {
                if (this.fieldModel != null)
                {
                    return this.fieldModel.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets an observable collection of quadrant results
        /// </summary>
        public ObservableCollection<QuadrantResultModel> Results
        {
            get { return this.results; }
        }

        /// <summary>
        /// Gets the current error message 
        /// </summary>
        public string ErrorMessage
        {
            get { return this.errorMessage; }
            private set { SetProperty(ref this.errorMessage, value); }
        }

        /// <summary>
        /// Reset the field rankings
        /// </summary>
        public void Reset()
        {
            field.Reset();
        }

        /// <summary>
        /// Query the field for results
        /// </summary>
        /// <param name="sender">
        /// The object that raised the dispatcher timeout
        /// </param>
        /// <param name="e">
        /// The event arguments
        /// </param>
        private void GetResults(object sender, EventArgs e)
        {
           Task.Run(new Func<Task>(GetResultsInBackground));
        }

        private async Task GetResultsInBackground()
        {
            if (this.field == null)
            {
                return;
            }

            var fieldResults = await this.field.QueryAsync();

            if (fieldResults.IsConfigured)
            {
                this.ErrorMessage = null;
                this.results.Clear();

                var quadrants = new List<Quadrant> { fieldResults.Yellow, fieldResults.Red, fieldResults.Blue, fieldResults.Green };
                quadrants.Sort((p, q) => p.Rank - q.Rank);
                foreach (var q in quadrants)
                {
                    this.results.Add(new QuadrantResultModel(q));
                }
            }
            else
            {
                this.ErrorMessage = "No results to display";
            }
        }
        
        /// <summary>
        /// Create a new QuadrantResultModel with the given color and state
        /// </summary>
        /// <param name="color">The background color for the model</param>
        /// <param name="quad">The quadrant state for the model</param>
        /// <returns></returns>
        private QuadrantResultModel MakeQuadrant(string color, Quadrant quad)
        {
            return new QuadrantResultModel(color, quad.Rank, quad.IsSwitchOn, quad.DidTrigger);
        }
    }
}
