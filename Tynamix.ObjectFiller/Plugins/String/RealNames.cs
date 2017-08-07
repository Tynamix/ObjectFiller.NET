// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RealNames.cs" company="Tynamix">
//   © 2015 by Roman Köhler
// </copyright>
// <summary>
//   Generates real names like "Antonio Winter"
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Tynamix.ObjectFiller
{

    /// <summary>
    /// Style of the Name
    /// </summary>
    public enum NameStyle
    {
        /// <summary>
        /// Just generate first name
        /// </summary>
        FirstName,

        /// <summary>
        /// Just generate last name
        /// </summary>
        LastName,

        /// <summary>
        /// Generate first name and then last name
        /// </summary>
        FirstNameLastName,

        /// <summary>
        /// Generate last name and then first name
        /// </summary>
        LastNameFirstName
    }

    /// <summary>
    /// Generates real names like "Antonio Winter"
    /// </summary>
    public class RealNames : IRandomizerPlugin<string>
    {
        /// <summary>
        /// The style of the name to generate
        /// </summary>
        private readonly NameStyle nameStyle;

        /// <summary>
        /// All first names from the resource file
        /// </summary>
        private readonly string[] firstNames;

        /// <summary>
        /// All last names from the resource file
        /// </summary>
        private readonly string[] lastNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealNames"/> class.
        /// </summary>
        public RealNames()
            : this(NameStyle.FirstNameLastName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RealNames"/> class.
        /// </summary>
        /// <param name="nameStyle">
        /// The style how the name shall be generated.
        /// </param>
        public RealNames(NameStyle nameStyle)
        {
            this.nameStyle = nameStyle;

            if (this.nameStyle != NameStyle.LastName)
            {
                this.firstNames = Resources.FirstNames.Split(';');
            }

            if (this.nameStyle != NameStyle.FirstName)
            {
                this.lastNames = Resources.LastNames.Split(';');
            }
        }

        /// <summary>
        /// Gets random data for type <see cref="string"/>
        /// </summary>
        /// <returns>Random data for type <see cref="string"/></returns>
        public string GetValue()
        {
            if (this.nameStyle == NameStyle.FirstNameLastName || this.nameStyle == NameStyle.LastNameFirstName)
            {
                string firstName = this.firstNames[Random.Next(this.firstNames.Length)];
                string lastName = this.lastNames[Random.Next(this.lastNames.Length)];


                return this.nameStyle == NameStyle.FirstNameLastName ? firstName + " " + lastName : lastName + " " + firstName;
            }

            if (this.nameStyle == NameStyle.FirstName)
            {
                return this.firstNames[Random.Next(this.firstNames.Length)];
            }

            if (this.nameStyle == NameStyle.LastName)
            {
                return this.lastNames[Random.Next(this.lastNames.Length)];
            }

            return null;
        }
    }
}
