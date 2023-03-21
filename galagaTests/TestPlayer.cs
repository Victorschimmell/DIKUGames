using Galaga;

namespace GalagaTests {
    [TestFixture]
    public class TestPlayer {

    [SetUp]
    public void InitiatePlayer() {
        Player testPlayer = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        testPlayer.CreateOpenGLContext();
        
    }

    [Test]
    public void TestNoMove() {
        Assert.AreEqual(TestPlayer.Shape.Position, new Vec2F(0.45f, 0.1f));
    }

    [Test]
    public void TestMoveRight() {

    }

    [Test]
    public void TestMoveLeft() {

    }

    [Test]
    public void TestMoveUp() {

    }

    [Test]
    public void TestMoveDown() {

    }
}
}