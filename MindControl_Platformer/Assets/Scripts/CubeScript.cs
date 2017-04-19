using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour
{
    private float MinValue = 0.3F;
    private float speed = 0.05F;
    private float autoMoveSpeed = 0.05F;
    private Vector3 initPos;

    private Color tempColor;

    public void Start()
    {
        initPos = this.transform.position;
        tempColor = GetComponent<Renderer>().material.color;
    }

    public void Update()
    {
        Vector3 pos = this.transform.position;
        bool action = false;

        if (Mathf.Abs(pos.z - initPos.z) < 5.5F)
        {
            if (EmoMentalCommand.MentalCommandActionPower[1] > MinValue) //push
            {
                speed = EmoMentalCommand.MentalCommandActionPower[1] / 10;
                pos.z += speed;
                this.transform.position = pos;
                action = true;
            }

            if (EmoMentalCommand.MentalCommandActionPower[2] > MinValue) // pull
            {
                speed = EmoMentalCommand.MentalCommandActionPower[2] / 10;
                pos.z -= speed;
                this.transform.position = pos;
                action = true;
            }
        }

        if (Mathf.Abs(pos.y - initPos.y) < 3.5F)
        {
            if (EmoMentalCommand.MentalCommandActionPower[3] > MinValue) // lift
            {
                speed = EmoMentalCommand.MentalCommandActionPower[3] / 10;
                pos.y += speed;
                this.transform.position = pos;
                action = true;
            }

            if (EmoMentalCommand.MentalCommandActionPower[4] > MinValue) // drop
            {
                speed = EmoMentalCommand.MentalCommandActionPower[4] / 10;
                pos.y -= speed;
                this.transform.position = pos;
                action = true;
            }
        }

        if (Mathf.Abs(pos.x - initPos.x) < 3.5F)
        {
            if (EmoMentalCommand.MentalCommandActionPower[5] > MinValue)// left
            {
                speed = EmoMentalCommand.MentalCommandActionPower[5] / 10;
                pos.x -= speed;
                this.transform.position = pos;
                action = true;
            }

            if (EmoMentalCommand.MentalCommandActionPower[6] > MinValue) // right
            {
                speed = EmoMentalCommand.MentalCommandActionPower[6] / 10;
                pos.x += speed;
                this.transform.position = pos;
                action = true;
            }
        }

        if (EmoMentalCommand.MentalCommandActionPower[7] > 0.5F) // rotate left
        {
            speed = EmoMentalCommand.MentalCommandActionPower[7] / 10;
            this.transform.RotateAround(Vector3.up, speed);
        }

        if (EmoMentalCommand.MentalCommandActionPower[8] > MinValue) // rotate right
        {
            speed = EmoMentalCommand.MentalCommandActionPower[8] / 10;
            this.transform.RotateAround(Vector3.up, -speed);
        }

        if (EmoMentalCommand.MentalCommandActionPower[9] > MinValue) // clockwise
        {
            speed = EmoMentalCommand.MentalCommandActionPower[9] / 10;
            this.transform.RotateAround(Vector3.forward, -speed);
        }

        if (EmoMentalCommand.MentalCommandActionPower[10] > MinValue) // counter clockwise
        {
            speed = EmoMentalCommand.MentalCommandActionPower[10] / 10;
            this.transform.RotateAround(Vector3.forward, speed);
        }

        if (EmoMentalCommand.MentalCommandActionPower[11] > MinValue) // forward
        {
            speed = EmoMentalCommand.MentalCommandActionPower[11] / 10;
            this.transform.RotateAround(Vector3.right, speed);
        }

        if (EmoMentalCommand.MentalCommandActionPower[12] > MinValue) // reverse
        {
            speed = EmoMentalCommand.MentalCommandActionPower[12] / 10;
            this.transform.RotateAround(Vector3.right, -speed);
        }

        if (EmoMentalCommand.MentalCommandActionPower[13] > MinValue) // reverse
        {
            tempColor.a = 1 - EmoMentalCommand.MentalCommandActionPower[13];
            GetComponent<Renderer>().material.color = tempColor;
        }

        if (!action)
            MoveToPoint(pos, initPos);
    }

    private void MoveToPoint(Vector3 pos, Vector3 point)
    {
        if (pos.x - point.x >= autoMoveSpeed)
            pos.x -= autoMoveSpeed;
        else if (pos.x - point.x <= -autoMoveSpeed)
            pos.x += autoMoveSpeed;
        else
            pos.x = point.x;

        if (pos.y - point.y >= autoMoveSpeed)
            pos.y -= autoMoveSpeed;
        else if (pos.y - point.y <= -autoMoveSpeed)
            pos.y += autoMoveSpeed;
        else
            pos.y = point.y;

        if (pos.z - point.z >= autoMoveSpeed)
            pos.z -= autoMoveSpeed;
        else if (pos.z - point.z <= -autoMoveSpeed)
            pos.z += autoMoveSpeed;
        else
            pos.z = point.z;

        this.transform.position = pos;

        if (tempColor.a < 1F)
        {
            tempColor.a += 0.1F;
            GetComponent<Renderer>().material.color = tempColor;
        }
    }
}
