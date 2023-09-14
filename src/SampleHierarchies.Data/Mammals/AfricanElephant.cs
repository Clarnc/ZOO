using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals
{
    /// <summary>
    /// Very basic African elephant class.
    /// </summary>
    public class AfricanElephant : MammalBase, IAfricanElephant
    {
        #region Public Properties

        /// <inheritdoc/>
        public float height { get; set; }

        /// <inheritdoc/>
        public float weight { get; set; }

        /// <inheritdoc/>
        public float tuskLength { get; set; }

        /// <inheritdoc/>
        public int lifespan { get; set; }

        /// <inheritdoc/>
        public string socialBehavior { get; set; }

        #endregion // Public Properties

        #region Constructors

        /// <summary>
        /// Constructor for the AfricanElephant class.
        /// </summary>
        /// <param name="name">Name of the African elephant</param>
        /// <param name="age">Age of the African elephant</param>
        /// <param name="height">Height of the African elephant</param>
        /// <param name="weight">Weight of the African elephant</param>
        /// <param name="tuskLength">Tusk length of the African elephant</param>
        /// <param name="lifespan">Lifespan of the African elephant</param>
        /// <param name="socialBehavior">Social behavior of the African elephant</param>
        public AfricanElephant(string name, int age, float height, float weight, float tuskLength, int lifespan, string socialBehavior)
            : base(name, age, MammalSpecies.AfricanElephant)
        {
            this.height = height;
            this.weight = weight;
            this.tuskLength = tuskLength;
            this.lifespan = lifespan;
            this.socialBehavior = socialBehavior;
        }

        #endregion // Constructors

        #region Public Methods

        /// <inheritdoc/>
        public override void MakeSound()
        {
            Console.WriteLine("I am an African elephant named {0}, and I am trumpeting!", Name);
        }

        /// <inheritdoc/>
        public override void Move()
        {
            Console.WriteLine("I am an African elephant named {0}, and I am walking!", Name);
        }

        /// <inheritdoc/>
        public override void Display()
        {
            Console.WriteLine($"I am an African elephant named {Name}, my age is {Age}, my height is {height} meters, my weight is {weight} kilograms, my tusk length is {tuskLength} meters, my lifespan is {lifespan} years, and my social behavior is '{socialBehavior}'.");
        }

        /// <inheritdoc/>
        public override void Copy(IAnimal animal)
        {
            if (animal is IAfricanElephant elephant)
            {
                base.Copy(animal);
                height = elephant.height;
                weight = elephant.weight;
                tuskLength = elephant.tuskLength;
                lifespan = elephant.lifespan;
                socialBehavior = elephant.socialBehavior;
            }
        }

        #endregion // Public Methods
    }
}
