using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class SphereController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    // Start()はゲームオブジェクトが生成されたあとで1回だけ呼ばれる
    private void Start()
    {
        // 入力に応じてSphereに力を加えたいため、オブジェクトにアタッチされているRigidbodyコンポーネントを取得しておく
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update()は毎フレーム（画面が描画されるごとに）呼ばれる
    private void Update()
    {
        // ゲームパッドが接続されているかどうかのチェック
        if (Gamepad.current != null)
        {
            // ゲームパッドが接続されていればここに入る

            // ゲームパッドのスティックの傾きは、Vector2型で取得できる
            var leftStickValue = Gamepad.current.leftStick.value;
            Debug.Log("leftStick の傾き");
            Debug.Log(leftStickValue);
            // スティックの傾きに応じて、Sphereに力を加えてみる
            _rigidbody.AddForce(new Vector3(leftStickValue.x, 0, leftStickValue.y));

            // ゲームパッドの右側に配置されている4つのボタンは、 buttonNorth・buttonEast・buttonSouth・buttonWest で指定できる
            // leftButton や leftTrigger などで、トリガーボタン類も指定可能

            // ボタン名の後ろに wasPressedThisFrame をつけると、「そのボタンが今の瞬間に押されたかどうか」を判定
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                Debug.Log("ゲームパッドの buttonSouth がいま押されたよ！");
                Jump();
            }

            // ボタン名の後ろに isPressed をつけると、「そのボタンが押されている状態かどうか」を判定
            if (Gamepad.current.buttonSouth.isPressed) Debug.Log("ゲームパッドの buttonSouth が押され続けているよ！");

            // ボタン名の後ろに wasReleasedThisFrame をつけると、「そのボタンが今の瞬間に離されたかどうか」を判定
            if (Gamepad.current.buttonSouth.wasReleasedThisFrame) Debug.Log("ゲームパッドの buttonSouth がいま離されたよ！");
        }

        // キーボードがあるかどうかのチェック
        if (Keyboard.current != null)
        {
            // キーボードがあればここに入る
            // Keyboard.current.{キー名小文字}Key または Keyboard.current[Key.{キー名}] で、各キーの状態がとれる

            if (Keyboard.current.wKey.isPressed)
            {
                Debug.Log("キーボードの「W」キーが押され続けているよ！");
                // Sphereに奥向きの力を加えてみる
                _rigidbody.AddForce(new Vector3(0, 0, 1));
            }

            if (Keyboard.current[Key.A].isPressed)
            {
                Debug.Log("キーボードの「A」キーが押され続けているよ！");
                // Sphereに左向きの力を加えてみる
                _rigidbody.AddForce(new Vector3(-1, 0, 0));
            }

            if (Keyboard.current.sKey.isPressed)
            {
                Debug.Log("キーボードの「S」キーが押され続けているよ！");
                // Sphereに手前向きの力を加えてみる
                _rigidbody.AddForce(new Vector3(0, 0, -1));
            }

            if (Keyboard.current[Key.D].isPressed)
            {
                Debug.Log("キーボードの「D」キーが押され続けているよ！");
                // Sphereに右向きの力を加えてみる
                _rigidbody.AddForce(new Vector3(1, 0, 0));
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("キーボードの「スペース」キーがいま押されたよ！");
                Jump();
            }
        }

        // マウスやトラックパッドがあるかどうかのチェック
        if (Mouse.current != null)
        {
            // マウスやトラックパッドがあればここに入る
            // position で画面上のマウス座標、leftButton や rightButton でボタンの状態、scroll でホイールの状態が取得できる

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Debug.Log("マウスの「左ボタン」がいま押されたよ！");
                Jump();
            }

            if (Mouse.current.rightButton.isPressed)
            {
                Debug.Log("マウスの「右ボタン」が押され続けているよ！");
                // マウス座標は下記のような形で取得できる
                var mousePos = Mouse.current.position.value;
                Debug.Log($"マウス座標 x:{mousePos.x} y:{mousePos.y}");
            }

            if (Mouse.current.scroll.value.magnitude > 0f)
            {
                var scrollValue = Mouse.current.scroll.value;
                Debug.Log($"マウスのホイールを回したよ！ x:{scrollValue.x} y:{scrollValue.y}");
            }
        }
    }

    private void Jump()
    {
        // Sphereに上向きの力を加えてみる
        _rigidbody.AddForce(new Vector3(0, 100, 0));
    }
}