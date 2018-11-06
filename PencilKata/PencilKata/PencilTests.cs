using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnitLite;
using PencilLibrary;

namespace PencilKata
{
    [TestFixture]
    class PencilTests
    {
        Pencil pencil;

        static void Main(string[] args)
        {
            new AutoRun().Execute(args);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        [SetUp]
        public void testInit()
        {
            pencil = new Pencil(100, 100, 100);
        }

        [Test]
        public void whenPencilIsPassedTwoStringsItAppendsTheFirstStringOntoTheSecondString()
        {
            Assert.AreEqual("aaaSome Test Words", pencil.Write("Some Test Words", "aaa"));
        }

        [Test]
        public void whenALowercaseLetterIsWrittenDurabilityDegradesByOne()
        {

            pencil.Write("  a b \n ");

            Assert.AreEqual(98, pencil.getPointDurability());

            pencil.Write("  c   ");

            Assert.AreEqual(97, pencil.getPointDurability());
        }

        [Test]
        public void whenAnUppercaseLetterIsWrittenDurabilityDegradesByTwo()
        {

            pencil.Write("A \nB");

            Assert.AreEqual(96, pencil.getPointDurability());

            pencil.Write(" C   ");

            Assert.AreEqual(94, pencil.getPointDurability());

        }

        [Test]
        public void whenSharpenedDurabilityIsReset()
        {
            pencil.Write("fubdfniebibnasdA");

            pencil.Sharpen();

            Assert.AreEqual(100, pencil.getMaxDurability());
            Assert.AreEqual(100, pencil.getPointDurability());
        }

        [Test]
        public void whenThereIsNotEnoughDurabilityABlankIsWritten()
        {
            pencil = new Pencil(5, 0, 0);

            Assert.AreEqual("abcd f  ", pencil.Write("abcdEfgh"));


        }

        [Test]
        public void whenPencilIsSharpenedLengthIsReducedByOne()
        {
            pencil = new Pencil(5, 1, 0);

            pencil.Write("aaaaa");
            pencil.Sharpen();

            Assert.AreEqual(0, pencil.getLength());

        }

        [Test]
        public void whenPencilLengthIsZeroPencilCannotBeSharpened()
        {
            pencil = new Pencil(5, 0, 0);

            pencil.Write("aaaaa");
            pencil.Sharpen();

            Assert.AreEqual("     ", pencil.Write("aaaaa"));

        }

        [Test]
        public void whenGivenTwoStringsReplaceTheLastInstanceOfTheFirstStringInTheSecondStringWithBlankSpaces()
        {
            Assert.AreEqual("Erase last instance of word          in this string", pencil.Erase("instance", "Erase last instance of word instance in this string"));
        }

        [Test]
        public void whenGivenAStringToEraseThatIsNotInTheStartingStringReturnTheStartingString()
        {
            Assert.AreEqual("Nothing should erase", pencil.Erase("bacon", "Nothing should erase"));
        }

        [Test]
        public void whenEraserIsUsedDegradeEraserDurabilityByOnePerCharacter()
        {
            pencil = new Pencil(5, 1, 10);

            pencil.Erase(" Durability", "Reduce Durability");

            Assert.AreEqual(0, pencil.getEraserDurability());
        }

        [Test]
        public void whenEraserDurabilityIsZeroErasingStops()
        {
            pencil = new Pencil(5, 1, 8);

            Assert.AreEqual("Reduce Du        ", pencil.Erase(" Durability", "Reduce Durability"));
        }

        [Test]
        public void whenGivenAStringEditWritesThatStringInTheFirstBlankSpace()
        {
            Assert.AreEqual("Edit Test Complete", pencil.Edit("Test", "Edit      Complete"));
        }

        [Test]
        public void whenEditingASpaceWithAStringThatIsTooLongUseTheAtSymbolInstead()
        {
            Assert.AreEqual("This is no@ight", pencil.Edit("not", "This is   right"));
        }

        [Test]
        public void whenNonLetterCharactersAreWrittenPointDurabilityDegradesByOne()
        {
            pencil.Write("!1,./'-_=0");
            Assert.AreEqual(90, pencil.getPointDurability());
        }

        [Test]
        public void whenEditingAPaperPointStillDegrades()
        {
            pencil.Edit("Test", "Edit      Complete");
            Assert.AreEqual(95, pencil.getPointDurability());
        }

        [Test]
        public void whenPointDurabilityIsAtZeroEditingStops()
        {
            pencil = new Pencil(3, 0, 0);

            Assert.AreEqual("Edit Te   Complete", pencil.Edit("Test", "Edit      Complete"));
        }

        [Test]
        public void whenPassedIntegerNEditWillEditTheNthBlankSpace()
        {
            Assert.AreEqual("Edit      Complete Test Again", pencil.Edit("Test", "Edit      Complete      Again", 2));
        }

        [Test]
        public void whenPassedABlankSpaceThatDoesNotExistEditWillNotEditAnything()
        {
            Assert.AreEqual("This     Should     Not     Change", pencil.Edit("aaa", "This     Should     Not     Change", 20));
        }

        [Test]
        public void whenGivenAStringThatStartsWithABlankSpaceEditWillNotLeaveABlankAtTheBeginningOfTheString()
        {
            Assert.AreEqual("False: this string begins with a blank space", pencil.Edit("False:", "       this string begins with a blank space"));
        }

        [Test]
        public void whenEditingPointDegradesBasedOnWhatIsWritten()
        {
            pencil.Edit("A b C d", "Test        String");

            Assert.AreEqual(94, pencil.getPointDurability());
        }
    }
}
