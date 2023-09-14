using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;
using System;

namespace SampleHierarchies.Data.Mammals
{
    /// <summary>
    /// Class representing a Chimpanzee.
    /// </summary>
    public class Chimpanzee : MammalBase, IChimpanzee
    {
        #region Public Properties

        /// <inheritdoc/>
        public bool opposableThumbs { get; set; }

        /// <inheritdoc/>
        public string socialBehavior { get; set; }

        /// <inheritdoc/>
        public bool toolUse { get; set; }

        /// <inheritdoc/>
        public int intelligence { get; set; }

        /// <inheritdoc/>
        public string diet { get; set; }

        #endregion // Public Properties

        #region Constructors

        /// <summary>
        /// Constructor for the Chimpanzee class.
        /// </summary>
        /// <param name="name">Name of the Chimpanzee</param>
        /// <param name="age">Age of the Chimpanzee</param>
        /// <param name="opposableThumbs">Whether the Chimpanzee has opposable thumbs</param>
        /// <param name="socialBehavior">Social behavior of the Chimpanzee</param>
        /// <param name="toolUse">Whether the Chimpanzee uses tools</param>
        /// <param name="intelligence">Intelligence level of the Chimpanzee</param>
        /// <param name="diet">Diet of the Chimpanzee</param>
        public Chimpanzee(string name, int age, bool opposableThumbs, string socialBehavior, bool toolUse, int intelligence, string diet)
            : base(name, age, MammalSpecies.Chimpanzee)
        {
            this.opposableThumbs = opposableThumbs;
            this.socialBehavior = socialBehavior;
            this.toolUse = toolUse;
            this.intelligence = intelligence;
            this.diet = diet;
        }

        #endregion // Constructors

        #region Public Methods

        /// <inheritdoc/>
        public override void MakeSound()
        {
            Console.WriteLine($"My name is: {Name} and I am making chimpanzee sounds");
        }

        /// <inheritdoc/>
        public override void Move()
        {
            Console.WriteLine($"My name is: {Name} and I am swinging from trees");
        }

        /// <inheritdoc/>
        public override void Display()
        {
            Console.WriteLine($"My name is: {Name}, my age is: {Age}, I have opposable thumbs: {opposableThumbs}, my social behavior is {socialBehavior}, " +
                      $"I use tools: {toolUse}, my intelligence level is {intelligence}, and my diet is {diet}.");
        }

        /// <inheritdoc/>
        public override void Copy(IAnimal animal)
        {
            if (animal is IChimpanzee chimpanzee)
            {
                base.Copy(animal);
                opposableThumbs = chimpanzee.opposableThumbs;
                socialBehavior = chimpanzee.socialBehavior;
                toolUse = chimpanzee.toolUse;
                intelligence = chimpanzee.intelligence;
                diet = chimpanzee.diet;
            }
        }

        #endregion // Public Methods
    }
}
