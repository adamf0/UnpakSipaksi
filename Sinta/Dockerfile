# Gunakan Python 3.10.12 sebagai base image
FROM python:3.10.12

# Atur working directory dalam container
WORKDIR /app

# Salin file requirements.txt ke dalam container
COPY main.py .
COPY requirements.txt .

# Pastikan pip diperbarui ke versi 22.0.2
RUN pip install --no-cache-dir --upgrade pip==22.0.2

# Install dependencies dari requirements.txt
RUN pip install --no-cache-dir -r requirements.txt

# Salin seluruh kode proyek ke dalam container
COPY . .

# Jalankan aplikasi
CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000"]
