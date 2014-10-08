namespace BESTTieBreaker.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Timers;
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
        /// The UI thread dispatcher to handle PropertyChanged events
        /// </summary>
        private Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        /// <summary>
        /// The query timer
        /// </summary>
        private Timer pollTimer;

        /// <summary>
        /// Error message to display
        /// </summary>
        private string errorMessage = "No field connected";

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
                    pollTimer.Elapsed -= this.GetResults;
                    pollTimer.Dispose();
                }

                SetProperty(ref this.fieldModel, value);

                RaisePropertyChanged("Id");

                if (this.fieldModel != null)
                {
                    this.field = this.fieldModel.Field;

                    this.pollTimer = new Timer(250.0);
                    this.pollTimer.AutoReset = true;
                    this.pollTimer.Elapsed += this.GetResults;
                    this.pollTimer.Start();
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
            if (this.field == null)
            {
                return;
            }

            var fieldResults = this.field.Query();

            this.dispatcher.BeginInvoke(
                new Action<FieldState>(this.DisplayResults),
                fieldResults);
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

        /// <summary>
        /// Updates display with new results
        /// </summary>
        /// <param name="fieldResults">The new field state</param>
        private void DisplayResults(FieldState fieldResults)
        {
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
    }
}
