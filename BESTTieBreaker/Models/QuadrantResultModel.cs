namespace BESTTieBreaker.Models
{
    using System.Windows;
    using System.Windows.Media;

    using BEST2014;

    public class QuadrantResultModel : Model
    {
        /// <summary>
        /// The color of the field quadrant
        /// </summary>
        private string color;

        /// <summary>
        /// The ranking for this quadrant
        /// </summary>
        private int rank;

        /// <summary>
        /// Value indicating whether the tie-breaker switch is currently on
        /// </summary>
        private bool isTriggered;

        /// <summary>
        /// Value indicating whether the tie-breaker switch was active since the past reset
        /// </summary>
        private bool didTrigger;

        public QuadrantResultModel(string color, int rank, bool isTriggered, bool didTrigger)
        {
            this.color = color;
            this.rank = rank;
            this.isTriggered = isTriggered;
            this.didTrigger = didTrigger;
        }

        public QuadrantResultModel(Quadrant quad)
        {
            this.color = quad.Color;
            this.rank = quad.Rank;
            this.isTriggered = quad.IsSwitchOn;
            this.didTrigger = quad.IsSwitchOn || quad.Rank < 4;
        }

        /// <summary>
        /// Gets the color of the field quadrant
        /// </summary>
        public string Color
        {
            get { return this.color; }
        }

        /// <summary>
        /// Gets or sets the rank of the field quadrant
        /// </summary>
        public int Rank
        {
            get { return this.rank; }
            set { SetProperty(ref this.rank, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the tie-breaker switch is currently on
        /// </summary>
        public bool IsTriggered
        {
            get { return this.isTriggered; }
        }

        /// <summary>
        /// Gets a value indicating whether the tie-breaker switch was active since the past reset
        /// </summary>
        public bool DidTrigger
        {
            get { return this.didTrigger; }
        }
    }
}
