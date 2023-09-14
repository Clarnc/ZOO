using System;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals
{
    /// <summary>
    /// Basic Polar Bear class.
    /// </summary>
    public class PolarBear : MammalBase, IPolarBear
    {
        #region Public Methods

        /// <inheritdoc/>
        public override void MakeSound()
        {
            Console.WriteLine("My name is: {0} and I am roaring", Name);
        }

        /// <inheritdoc/>
        public override void Move()
        {
            Console.WriteLine("My name is: {0} and I am walking", Name);
        }

        /// <inheritdoc/>
        public override void Display()
        {
            Console.WriteLine($"My name is: {Name}, my age is: {Age}, I have a {furCoat} fur coat, {paws} paws, a {carnivorousDiet} diet, " +
                      $"I am {(semiAquatic ? "semi-aquatic" : "not semi-aquatic")}, and my sense of smell is {senceOfSmell}.");
        }

        /// <inheritdoc/>
        public override void Copy(IAnimal animal)
        {
            if (animal is IPolarBear pb)
            {
                base.Copy(animal);
                furCoat = pb.furCoat;
                paws = pb.paws;
                carnivorousDiet = pb.carnivorousDiet;
                semiAquatic = pb.semiAquatic;
                senceOfSmell = pb.senceOfSmell;
            }
        }

        #endregion // Public Methods

        #region Ctors And Properties

        /// <inheritdoc/>
        public string furCoat { get; set; }
        /// <inheritdoc/>
        public string paws { get; set; }
        /// <inheritdoc/>
        public string carnivorousDiet { get; set; }
        /// <inheritdoc/>
        public bool semiAquatic { get; set; }
        /// <inheritdoc/>
        public string senceOfSmell { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="furCoat">Fur Coat</param>
        /// <param name="paws">Paws</param>
        /// <param name="carnivorousDiet">Carnivorous Diet</param>
        /// <param name="semiAquatic">SemiAquatic</param>
        /// <param name="senceOfSmell">Sense of Smell</param>
        public PolarBear(string name, int age, string furCoat, string paws, string carnivorousDiet, bool semiAquatic, string senceOfSmell) : base(name, age, MammalSpecies.PolarBear)
        {
            this.furCoat = furCoat;
            this.paws = paws;
            this.carnivorousDiet = carnivorousDiet;
            this.semiAquatic = semiAquatic;
            this.senceOfSmell = senceOfSmell;
        }

        #endregion // Ctors And Properties
    }
}
