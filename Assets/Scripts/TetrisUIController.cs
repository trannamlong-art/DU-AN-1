using UnityEngine;

public class TetrisUIController : MonoBehaviour
{
    private TetrisBlock currentBlock;

    public void SetCurrentBlock(TetrisBlock block)
    {
        currentBlock = block;
    }

    public void OnMoveLeft()
    {
        if (currentBlock != null)
            currentBlock.Move(Vector3.left);
    }

    public void OnMoveRight()
    {
        if (currentBlock != null)
            currentBlock.Move(Vector3.right);
    }

    public void OnMoveDown()
    {
        if (currentBlock != null)
            currentBlock.Move(Vector3.down);
    }

    public void OnRotate()
    {
        if (currentBlock != null)
            currentBlock.Rotate();
    }
}
