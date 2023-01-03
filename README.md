# Curse_of_Druid
2022 겨울방학 게임 개발 프로젝트

## 프로그래밍 관련 안내사항

### 네이밍 규칙

- 변수명은 camelCase로 쓰기 (snake case 쓰지말기)
- 변수명은 의미를 확실히 알 수 있게
- boolean 변수 이름은 is나 has로 시작
- 함수명은 PascalCase로 쓰기
- 함수명은 동사로 시작

```csharp
private int speed = 10f;
private int Speed = 10f; // Wrong example
private bool isAlive = false;
private bool alive = true; // Wrong example
private int AddItem(Item item){}
private int ItemAdder(Item item){} // Wrong example
public int Speed => speed;
```

### 코딩 스타일

- 들여쓰기는 스페이스 4번
- 중괄호 스타일은 C#은 일반적으로 BSD 사용

```csharp
for(int i = 0; i < 10; i++){
  int a = 1;
  int b = 2;
  // To-do
} // Wrong example

for(int i = 0; i < 10; i++)
{
    int a = 1;
    int b = 2;
    // To-do
}
```

---

## 깃 관련 안내사항

- 각자 브랜치를 생성해서 작업(main에서 작업하면 안됩니다)
- main에는 merge만
- merge는 자유롭게 하되 merge conflict나면 저에게 말해주세요
- scene은 각자 파서 작업해주세요

---

## 일정 관련 안내사항

- 1/5(목요일)까지 구조 작업하고 일 배분할 예정
- 게임잼 이슈로 금요일부터 일요일까지 메인플머 작업 못합니다 ㅠㅠ
- 질문 있으면 메인플머에게
