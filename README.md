# 🚀 HAHATalk (WPF MVVM Study & Extension Project)

본 프로젝트는 **'까불이 코더'**님의 WPF 카카오톡 클론 코딩 프로젝트를 기반으로 학습하며, 이를 실무적인 관점에서 **기능 확장 및 아키텍처 개선**을 진행한 프로젝트입니다.

단순한 UI 모방을 넘어, **WinForms 개발자로서 WPF의 MVVM 패러다임을 완벽히 이해하고 실무 DB(MSSQL) 환경에 맞게 재설계**하는 데 중점을 두었습니다.

---

## 🛠 주요 개선 및 확장 사항 (Customizing)

원천 프로젝트의 구조를 바탕으로 본인이 직접 설계하고 구현한 차별화 포인트입니다.

### 1. Database & Persistence Layer 전환
* **DB 환경 변경**: 기존 MySQL 환경을 실무 MES 환경에서 주로 쓰이는 **MSSQL**로 전면 교체.
* **Repository 패턴 고도화**: MSSQL 환경에 최적화된 `IAccountRepository` 인터페이스 재설계 및 `Dapper` 기반의 CRUD 로직 구현.

### 2. 신규 비즈니스 로직 및 기능 추가
* **비밀번호 변경 (Password Change)**: 
  - 현재 비밀번호 검증 및 신규 비밀번호 일치 여부 확인 로직 구현.
  - `Dictionary<string, bool>`를 활용하여 각 입력 필드의 유효성 상태를 실시간으로 관리.
  - 관련 MSSQL UPDATE 쿼리 및 프로시저 대응 함수 구현.
* **계정 찾기 (Find Account)**:
  - 계정 정보 확인을 위한 전용 화면 UI 설계 및 ViewModel 연동.
  - 이메일 및 사용자 정보를 기반으로 한 조회 쿼리 반영.

---

## 📌 주요 기술 스택 및 개념
* **Framework**: .NET 8 / WPF
* **Pattern**: MVVM (CommunityToolkit.Mvvm)
* **Validation**: `DataTrigger`와 `Dictionary Binding`을 통한 선언적 UI 피드백.
* **Asynchronous**: UI 프리징 방지를 위한 비동기 처리 구조 학습.

---

## 💡 개발자의 한마디
"5년간의 WinForms 개발 경험을 통해 쌓은 DB 설계 및 비즈니스 로직 구현 능력을 바탕으로, 
WPF의 현대적인 MVVM 패턴을 성공적으로 이식했습니다. 
오픈 소스 강의를 학습의 마중물로 삼아, 실제 현장에서 요구되는 
**데이터 정합성 검사**와 **확장 가능한 Repository 구조**로 발전시키는 과정을 담았습니다."
