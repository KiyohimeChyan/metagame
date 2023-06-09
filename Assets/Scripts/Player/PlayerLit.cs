using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class PlayerLit : MonoBehaviour
{
    public PlayerInputControlls inputControll;
    public Vector2 inputDirection;

    public float moveSpeed;
    public float jumpForce;

    public int currentHP;
    bool isDead;

    private Rigidbody2D rb;
    private PhysicsCheck pc;

    [Header("UI相关")]
    public TMP_Text hpText;
    public GameObject gameOverPanel;
    public Button selectedButton;

    public GameObject NPCDialog;

    public PlayerInputControlls inputs;


    public TextAsset dialogDataFile;

    //对话内容文本
    public TMP_Text dialogText;
    public TMP_Text nameText;

    //对话文本-按行分割
    public string[] dialogRows;

    //对话索引
    public int dialogIndex;

    //对话图片
    public List<Sprite> sprites = new List<Sprite>();
    public Image speakingProfile;

    string at;
    string comma;

    public float pressTimer;
    float pressTime;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PhysicsCheck>();

        inputControll = new PlayerInputControlls();
        //跳跃
        inputControll.Gameplay.Jump.started += Jump;
        inputControll.Gameplay.Dialog.started += OnDialogChange;
    }
    private void OnEnable()
    {
        inputControll.Enable();
    }

    private void OnDisable()
    {
        inputControll.Disable();
    }

    void Start()
    {
        at = "@";
        comma = ",";
        if (PlayerPrefs.GetInt("isDialog") == 0)
        {
            NPCDialog.SetActive(true);
        }
        ReadText(dialogDataFile);
        ShowDialog();
    }

    private void Update()
    {
        pressTime -= Time.deltaTime;
        inputDirection = inputControll.Gameplay.Move.ReadValue<Vector2>();
        hpText.text = "HP:" + currentHP;
        if (currentHP <= 0)
        {
            isDead = true;
            gameOverPanel.SetActive(true);
        }

    }

    private void FixedUpdate()
    {
        if(!isDead)
            Move();

    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (pc.isGround&&!isDead)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

        }
    }
    private void Move()
    {
        //int faceDir = (int)transform.localScale.x;
        //if (inputDirection.x > 0)
        //    faceDir = 1;
        //if (inputDirection.x < 0)
        //    faceDir = -1;
        //transform.localScale = new Vector3(faceDir, 1, 1);
        rb.velocity = new Vector2(inputDirection.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHP = Mathf.Max(currentHP - 1, 0);
        }
        else if (collision.gameObject.CompareTag("Boundary"))
        {
            currentHP = 0;
        }
        if(currentHP <= 0)
        {
            selectedButton.Select();

        }
    }


    public void UpdateText(string _text)
    {
        dialogText.text = Regex.Replace(_text, at, comma);
    }

    public void UpdateName(string text)
    {
        nameText.text = text;
    }

    public void UpdateImage(int num)
    {
        speakingProfile.sprite = sprites[num];
    }

    public void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
        //foreach(var row in rows)
        //{
        //    string[] cell = row.Split(',');
        //}

    }

    public void ShowDialog()
    {
        for (int i = 0; i < dialogRows.Length; i++)
        {
            string[] cells = dialogRows[i].Split(',');
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateText(cells[2]);
                UpdateName(cells[6]);
                UpdateImage(int.Parse(cells[5]));
                dialogIndex = int.Parse(cells[3]);
                break;
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                NPCDialog.SetActive(false);

            }
        }
    }



    public void OptionEffect(string effect)
    {
        //if (effect == "M1")
        //{
        //    currentDistance = Mathf.Max(currentDistance - 1, 0);
        //}
        //if (effect == "P1")
        //{
        //    currentDistance += 1;
        //}

    }

    public void OnClickNext()
    {
        ShowDialog();
    }

    private void OnDialogChange(InputAction.CallbackContext obj)
    {
        if (pressTime <= 0)
        {
            ShowDialog();
            pressTime = pressTimer;

        }
    }

}
